IF OBJECT_ID('dbo.Proc_ImportCountry', 'P') IS NULL
EXEC('CREATE PROCEDURE dbo.Proc_ImportCountry AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Proc_ImportCountry] (@DataImportCountry [TYPE_COUNTRY_IMPORT] Readonly)
AS
BEGIN
	-- SET NOCOUNT ON ADDED TO PREVENT EXTRA RESULT SETS FROM
	-- INTERFERING WITH SELECT STATEMENTS.
	SET NOCOUNT ON;

    -- INSERT STATEMENTS FOR PROCEDURE HERE
	;MERGE Region R
	USING
	(
		SELECT DISTINCT dc.RegionName FROM @DataImportCountry dc

	) S ON S.RegionName =R.Name	
	WHEN NOT MATCHED THEN
	INSERT (Name) VALUES (S.RegionName)
	OUTPUT $ACTION,INSERTED.*;

	;MERGE Territory TERRI
	USING
	(
		SELECT DISTINCT T.TerritoryName , T2.Id AS RegionId FROM @DataImportCountry T
		 JOIN Region T2 ON T2.Name=T.RegionName
	)S ON S.TerritoryName=TERRI.Name
	WHEN NOT MATCHED  THEN
	INSERT (Name,RegionId) VALUES(S.TerritoryName,S.RegionId)
	WHEN MATCHED AND(TERRI.RegionID<>s.RegionId) THEN
	UPDATE set TERRI.RegionID = s.RegionId
	OUTPUT $ACTION,INSERTED.*;
	;MERGE Currency CU 
	USING 
	(
		SELECT DISTINCT TEMPCURR.Currency as curr FROM @DataImportCountry TEMPCURR
	)S ON S.curr = CU.Name
	WHEN NOT MATCHED THEN
	INSERT (Code,Name) VALUES(S.curr,S.curr)
	OUTPUT $ACTION,INSERTED.*;
	;MERGE [Language] LAN
	USING 
	(
		SELECT DISTINCT dic.DefaultLanguange FROM @DataImportCountry dic

	)S ON S.DefaultLanguange = LAN.Name
	WHEN NOT MATCHED THEN
	INSERT (Name,Code) VALUES(S.DefaultLanguange,S.DefaultLanguange)
	OUTPUT $ACTION,INSERTED.*;

	;merge [Country] con
	using 
	(
		SELECT DISTINCT  tempcoun.CountryName,tr.Id AS TerritoryId,l.Id AS LanguageId ,curre.Id AS curren FROM @DataImportCountry tempcoun
		join Territory tr ON tr.Name=tempcoun.TerritoryName
		join [Language] l ON l.Name=tempcoun.DefaultLanguange
		join Currency  curre ON curre.Name=tempcoun.Currency
		)S ON S.CountryName=con.Name
		WHEN NOT matched THEN
	insert (Name,TerritoryId,LanguageId,CurrencyId,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn)values(S.CountryName,S.TerritoryId,S.LanguageId,S.curren,1,getDate(),1,getDate())
	WHEN MATCHED AND (con.TerritoryId <> S.TerritoryId OR con.LanguageId <> S.LanguageId OR con.CurrencyId <> S.curren) THEN 
	UPDATE SET con.TerritoryId = S.TerritoryId , con.LanguageId = S.LanguageId , con.CurrencyId = S. curren
	OUTPUT $action,INSERTED.*;


END
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('dbo.Proc_ImportPayoutCurve', 'P') IS NULL
EXEC('CREATE PROCEDURE dbo.Proc_ImportPayoutCurve AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE  [dbo].[Proc_ImportPayoutCurve](@PayoutCurve  [TYPE_PAYOUTCURVE_IMPORT] Readonly)
AS
BEGIN
SET NOCOUNT ON;
;merge PayoutCurve pc
using 
(
	select Distinct PayoutCurve as PayoutCurveName ,pt.Id as PayoutCurveTypeId from @PayoutCurve tempcurve
	join PayoutCurveType pt on pt.Name=tempcurve.MultiplierType
)s on s.PayoutCurveName = pc.Name
when not matched then
insert (Name,PayoutCurveTypeId)values(s.PayoutCurveName,s.PayoutCurveTypeId)
When MATCHED AND (pc.PayoutCurveTypeId <> s.PayoutCurveTypeId) THEN
UPDATE set pc.PayoutCurveTypeId = s.PayoutCurveTypeId
output $action,INSERTED.*;

select pc.Id as PayoutCurveId into #PayoutCurveIds 
from @PayoutCurve payoutcurveDetails
join PayoutCurve pc on pc.Name=payoutcurveDetails.PayoutCurve

;merge PayoutCurveDetail t
using
(
	select pc.Id as PayoutCurveId, payoutcurveDetails.SlabId,payoutcurveDetails.Min,payoutcurveDetails.Max,payoutcurveDetails.Multiplier from @PayoutCurve payoutcurveDetails
	join PayoutCurve pc on pc.Name=payoutcurveDetails.PayoutCurve
)s on s.PayoutCurveId=t.PayoutCurveId and s.SlabId = t.SlabId
when not matched then
insert (PayoutCurveId,SlabId,Min,Max,Multiplier)values(s.PayoutCurveId,s.SlabId,s.Min,s.Max,s.Multiplier)
when matched and (t.[Min] <> s.[Min] OR ISNULL(t.[Max], 0) <> ISNULL(s.[Max], 0) OR t.Multiplier <> s.Multiplier) THEN 
 Update set t.[Min] = s.[Min], t.[Max] = s.[Max], t.Multiplier = s.Multiplier
when not matched BY SOURCE  AND (t.PayoutCurveId in (select PayoutCurveId from #PayoutCurveIds))  then  DELETE
output $action,INSERTED.*,DELETED.*;
END
GO
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 IF OBJECT_ID('dbo.Proc_ImportIncentivePercentage', 'P') IS NULL
EXEC('CREATE PROCEDURE dbo.Proc_ImportIncentivePercentage AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  PROCEDURE  [dbo].[Proc_ImportIncentivePercentage] (@IncentivePayout [TYPE_INCENTIVEPERCENTAGE_IMPORT] Readonly)
AS
BEGIN
SET NOCOUNT ON;
;merge PayoutPercentage pg
using 
(
	select Distinct PayoutPercentage as PayoutPercentageName from @IncentivePayout incentivepercentage
)s on s.PayoutPercentageName = pg.Name
when not matched then
insert (Name)values(s.PayoutPercentageName)
output $action,INSERTED.*;

select pg.Id as  PayoutPercentageId  into #PayoutPercentage
from @IncentivePayout incentivepercentageDetails
	join FrequencyDetail fd on fd.Name=incentivepercentageDetails.FrequencyDetails
	join PayoutPercentage pg on pg.Name = incentivepercentageDetails.PayoutPercentage


;merge PayoutPercentageDetail t
using
(
	select fd.Id as FrequencyDetailId,pg.Id as  PayoutPercentageId ,incentivepercentageDetails.Percentage as PayoutPercentage from @IncentivePayout incentivepercentageDetails
	join FrequencyDetail fd on fd.Name=incentivepercentageDetails.FrequencyDetails
	join PayoutPercentage pg on pg.Name = incentivepercentageDetails.PayoutPercentage
)s on s.PayoutPercentageId = t.PayoutPercentageId and s.FrequencyDetailId = t.FrequencyDetailId
when not matched then
insert (FrequencyDetailId,PayoutPercentageId,PayoutPercentage)values(s.FrequencyDetailId,s.PayoutPercentageId,s.PayoutPercentage)
When Matched and (t.FrequencyDetailId <> s.FrequencyDetailId or t.PayoutPercentage <> s.PayoutPercentage) then
update set t.PayoutPercentage=s.PayoutPercentage, t.FrequencyDetailId = s.FrequencyDetailId
when not matched by source and (t.PayoutPercentageId in(select PayoutPercentageId from #PayoutPercentage)) Then DELETE
output $action,INSERTED.*,DELETED.*;

END
GO
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('dbo.Proc_ImportRole', 'P') IS NULL
EXEC('CREATE PROCEDURE dbo.Proc_ImportRole AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  PROCEDURE [dbo].[Proc_ImportRole] (@tempRole  [TYPE_ROLE_IMPORT] Readonly)  
   
AS  
BEGIN  
  
SET NOCOUNT ON;  
;merge BusinessUnit bu  
using   
(  
 select Distinct te.BusinessUnit from @tempRole te  
)s on s.BusinessUnit = bu.Name  
when not matched then   
insert (Name)values(s.BusinessUnit)  
output $action,INSERTED.*;  
;merge [Role] t  
using   
(  
 select Distinct roletemp.Role as RoleName, c.Id as CountryId,b.Id as BusinessUnitId from @tempRole roletemp  
 join Country c on c.Name=roletemp.Country  
 join BusinessUnit b on b.Name= roletemp.BusinessUnit  
)s on s.RoleName = t.Name and s.CountryId = t.CountryId and s.BusinessUnitId = t.BusinessUnitId  
when not matched then  
insert (Name,CountryId,BusinessUnitId)values(s.RoleName,s.CountryId,s.BusinessUnitId)  
output $action,INSERTED.*;  
  
;merge [Plan] t  
using   
(  
 select Distinct roletemp.Role as RoleName, rol.Id as RoleId, roletemp.RoleTargetIncentive , roletemp.[Year], roletemp.StartDate, roletemp.EndDate
 from @tempRole roletemp  
 join Country c on c.Name=roletemp.Country  
 join BusinessUnit b on b.Name= roletemp.BusinessUnit  
 join [Role] rol on rol.Name = roletemp.Role and rol.CountryId = c.Id and rol.BusinessUnitId = b.Id  
)s on s.RoleId = t.RoleId  and s.Year=t.Year  
when not matched then  
	insert (Name,RoleId,TargetIncentivePercentage,[Status],[Year],StartDate,EndDate)values(s.RoleName,s.RoleId,s.RoleTargetIncentive,1,s.[Year],s.StartDate,s.EndDate) 
 when matched then
	update set t.StartDate=s.StartDate, t.EndDate=s.EndDate, t.TargetIncentivePercentage=s.RoleTargetIncentive
output $action,INSERTED.*;  
END
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('dbo.Proc_ImportRoleMeasure', 'P') IS NULL
EXEC('CREATE PROCEDURE dbo.Proc_ImportRoleMeasure AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  PROCEDURE [dbo].[Proc_ImportRoleMeasure](@tempDataImport [TYPE_ROLEMEASURE_IMPORT] Readonly)  
AS  
BEGIN  
 SET NOCOUNT ON;  
;merge PlanModifier t  
using  
(  
 select   
 DISTINCT ModifierName = roleMeasu.Measure,roleMeasu.KPIModifierName,roleMeasu.KPIModifierValue,  
  p.Id as PlanId, pc.Id as PayoutcurveId 
 from @tempDataImport roleMeasu  
 Join   
 (  
  Select ip.Id, ic.Name as CountryName, ib.Name as BusinessUnitName,  
  ir.Name as RoleName , ip.year from [Plan] ip  
  Join [Role] ir on ir.Id = ip.RoleId  
  Join Country ic on ic.Id = ir.CountryId  
  Join BusinessUnit ib on ib.Id = ir.BusinessUnitId
 ) p on p.CountryName = roleMeasu.Country and p.BusinessUnitName = roleMeasu.BusinessUnit and p.RoleName = roleMeasu.Role and  p.Year=roleMeasu.Year
 join PayoutCurve pc on pc.Name=roleMeasu.PayoutCurve  
 where roleMeasu.Modifier ='Yes'  
)s on s.PlanId=t.PlanId and s.ModifierName =t.ModifierName  
when not matched then  
insert (PlanId,ModifierName,PayoutCurveId,DataType,ValueType) values(s.PlanId,s.ModifierName,s.PayoutCurveId,s.KPIModifierName,s.KPIModifierValue)  
when matched and (t.DataType<>s.KPIModifierName or t.ValueType<>s.KPIModifierValue or t.PayoutCurveId <>s.PayoutCurveId) THEN   
update set t.DataType = s.KPIModifierName ,t.ValueType=s.KPIModifierValue,t.PayoutCurveId=s.PayoutCurveId  
output $action,INSERTED.*;  
  
SELECT  Country = Country,  
  BusinessUnit=BusinessUnit,  
  [Role]=[Role],  
  MeasureSequence = Max(MeasureSequence),  
  Measure=Measure,  
  Frequency=max(Frequency),  
  MeasureWeightage=max(MeasureWeightage),  
  PayoutCurve=max(PayoutCurve),  
  PayoutType=max(PayoutType),  
  IncentivePayout=max(IncentivePayout),  
  Modifier =max(Modifier),  
  Goal =max(Goal),  
  KPIModifierName= max(KPIModifierName),  
  KPIModifierValue= max(KPIModifierValue),  
  IsKPI =max(IsKPI) , 
  [Year]=[Year]
INTO #tempDataImport2  
FROM @tempDataImport   
GROUP BY Country,BusinessUnit,Role, Measure ,Year 
;merge PlanMeasure t  
using   
(  
 select   
 DISTINCT roleMeasu.Measure as MeasureName,f.Id as FrequencyId,pt.Id as PayoutTypeId, pg.Id as PayoutPercentage,pc.Id as PayoutcurveId,roleMeasu.KPIModifierName,roleMeasu.KPIModifierValue,  
  p.Id as PlanId,MeasureWeightage ,roleMeasu.MeasureSequence as SequenceM,(case when roleMeasu.Goal ='' or Goal='Yes' Then 1 Else 0 End) as Goal,(Case when roleMeasu.IsKPI ='' or roleMeasu.IsKPI ='No' Then 0 Else 1 END) as IsKPI 
 from #tempDataImport2 roleMeasu  
 Join   
 (  
  Select ip.Id, ic.Name as CountryName, ib.Name as BusinessUnitName, ir.Name as RoleName  ,ip.Year 
  from [Plan] ip  
  Join [Role] ir on ir.Id = ip.RoleId  
  Join Country ic on ic.Id = ir.CountryId  
  Join BusinessUnit ib on ib.Id = ir.BusinessUnitId  
 ) p on p.CountryName = roleMeasu.Country and p.BusinessUnitName = roleMeasu.BusinessUnit and p.RoleName = roleMeasu.Role   and  p.Year=roleMeasu.Year
 join Frequency f on f.Name= (case When roleMeasu.Frequency = 'Semi Annual' Then 'HalfYearly' ElSE  roleMeasu.Frequency END)  
 join PayoutType pt on pt.Name=roleMeasu.PayoutType  
 join PayoutCurve pc on pc.Name=roleMeasu.PayoutCurve  
 join PayoutPercentage pg on pg.Name=roleMeasu.IncentivePayout  
 where roleMeasu.Modifier <>'Yes' and roleMeasu.Modifier ='No'  
)s on s.PlanId= t.PlanId  and s.SequenceM=t.Sequence  
when not matched then  
insert (PlanId,[Sequence],MeasureName,FrequencyId,PayoutTypeId,PayoutPercentageId,PayoutCurveId,MeasureWeightage,HasGoal,DataType,ValueType,IsKPI) values(s.PlanId,s.SequenceM,s.MeasureName,s.FrequencyId,s.PayoutTypeId,s.PayoutPercentage,s.PayoutcurveId,s.
MeasureWeightage,s.Goal,s.KPIModifierName,s.KPIModifierValue,s.IsKPI)  
when matched and (t.HasGoal<>s.Goal or t.[Sequence]<> s.SequenceM or t.MeasureWeightage<>s.MeasureWeightage or t.FrequencyId <>s.FrequencyId or t.MeasureName<> s.MeasureName or t.PayoutTypeId <>s.PayoutTypeId or t.PayoutPercentageId<>s.PayoutPercentage or
 t.PayoutCurveId <>s.PayoutcurveId  or t.DataType<>s.KPIModifierName or t.ValueType <> s.KPIModifierValue or t.IsKPI <> s.IsKPI) THen  
Update set t.HasGoal=s.Goal,t.[Sequence] = s.SequenceM , t.MeasureWeightage = s.MeasureWeightage , t.FrequencyId = s.FrequencyId , t.MeasureName =  s.MeasureName , t.PayoutTypeId  = s.PayoutTypeId , t.PayoutPercentageId = s.PayoutPercentage ,
 t.PayoutCurveId = s.PayoutcurveId,t.DataType=s.KPIModifierName ,t.ValueType=s.KPIModifierValue,t.IsKPI = s.IsKPI  
output $action,INSERTED.*;  
end
GO
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('dbo.Proc_UserInsert', 'P') IS NULL
EXEC('CREATE PROCEDURE dbo.Proc_UserInsert AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  PROCEDURE [dbo].[Proc_UserInsert]
(
	@LoginID NVARCHAR(50),  
	@UserName NVARCHAR(50),  
	@CountryId INT,  
	@BusinessUnitId INT,
	@CityId INT,  			
	@RoleId INT,
	@Salary decimal(18,0)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;
	BEGIN TRY
		BEGIN TRANSACTION
		DECLARE @Id int
		INSERT INTO [User](LoginID,UserName,CountryId,BusinessUnitId,CityId,SaveSimulation,IsActive) 
		VALUES( @LoginID, @UserName, @CountryId,@BusinessUnitId,@CityId,1,1)

		SET @Id = SCOPE_IDENTITY()

		INSERT INTO UserRoleDetail (UserId,RoleId,StartDate,EndDate) 
		VALUES(@Id,@RoleId,'2018-01-01','2018-12-31') 
		
		INSERT INTO UserSalaryDetail (UserId,StartDate,EndDate,Salary) 
		values(@Id,'2018-01-01','2018-03-31', @Salary)
		
		INSERT INTO UserSalaryDetail (UserId,StartDate,EndDate,Salary) 
		values(@Id,'2018-04-01',Null,@Salary * 1.1)
		
		--INSERT INTO UserSalaryDetail (UserId,StartDate,EndDate,Salary) 
		--values(@Id,'01-JULY-2018','30-SEP-2018',@Salary *  1.21)  
		
		--INSERT INTO UserSalaryDetail (UserId,StartDate,EndDate, Salary) 
		--values(@Id,'01-OCT-2018',NULL,@Salary  * 1.331)  
		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_SEVERITY() AS ErrorSeverity, ERROR_STATE() AS ErrorState, ERROR_PROCEDURE() AS ErrorProcedure, ERROR_LINE() AS ErrorLine, ERROR_MESSAGE() AS ErrorMessage;
		ROLLBACK TRANSACTION
	END CATCH
	END
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_GetPayoutCurve]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_GetPayoutCurve] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Proc_GetPayoutCurve] 
(
@CountryId int
)
AS
BEGIN
If(@CountryId=0)
begin
	select t1.Name,
	t2.SlabId,
	t2.Min,
	t2.Max,
	t2.Multiplier,
	t3.Name as MultiplierType
	from PayoutCurve t1 
	inner join PayoutCurveDetail t2 on t1.Id = t2.PayoutCurveId
	join PayoutCurveType t3 on t1.PayoutCurveTypeId = t3.Id 
end
ELSE
begin
	select t1.Name,
	t2.SlabId,
	t2.Min,
	t2.Max,
	t2.Multiplier,
	t3.Name as MultiplierType
	from PayoutCurve t1 
	inner join PayoutCurveDetail t2 on t1.Id = t2.PayoutCurveId
	join PayoutCurveType t3 on t1.PayoutCurveTypeId = t3.Id 
	where t1.Id in 
	(
		select distinct pm.PayoutCurveId from [Role] r 
		join [Plan] p on p.RoleId= r.id
		join [PlanMeasure] pm on pm.PlanId = p.Id where r.CountryId = @CountryId
	) 
end
END
GO
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
IF OBJECT_ID('[dbo].[Proc_GetCountry]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_GetCountry] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Proc_GetCountry]
(
	@CountryId int
)
AS
BEGIN
If(@CountryId=0)
begin
	select t1.Name as RegionName,
	t2.Name as TerritoryName,
	t3.Name as CountryName,
	t4.Name as DefaultLanguage,
	t5.Name as Currency
	from Region t1 
	join Territory t2 on t1.Id = t2.RegionId
	join Country t3 on t2.Id = t3.TerritoryId
	join Language t4 on t3.LanguageId = t4.Id
	join Currency t5 on t3.CurrencyId = t5.Id
	order by t1.Name,t2.Name,t3.Name
	end
ELSE
begin
	select t1.Name as RegionName,
	t2.Name as TerritoryName,
	t3.Name as CountryName,
	t4.Name as DefaultLanguage,
	t5.Name as Currency
	from Region t1 
	join Territory t2 on t1.Id = t2.RegionId
	join Country t3 on t2.Id = t3.TerritoryId
	join Language t4 on t3.LanguageId = t4.Id
	join Currency t5 on t3.CurrencyId = t5.Id
	where t3.Id = @CountryId
	order by t1.Name,t2.Name,t3.Name
	end
END

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_GetIncentivePayout]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_GetIncentivePayout] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Proc_GetIncentivePayout]
(
@CountryId int
)
AS
BEGIN
If(@CountryId=0)
begin
	select t2.Name as PayoutPercentage,
	t3.Name as FrequencyDetails,
	t1.PayoutPercentage as Percentage
	from PayoutPercentageDetail t1 
	join PayoutPercentage t2 on t1.PayoutPercentageId = t2.Id
	join FrequencyDetail t3 on t1.FrequencyDetailId  = t3.Id	
end
ELSE
begin
	select t2.Name as PayoutPercentage,
	t3.Name as FrequencyDetails,
	t1.PayoutPercentage as Percentage
	from PayoutPercentageDetail t1 
	join PayoutPercentage t2 on t1.PayoutPercentageId = t2.Id
	join FrequencyDetail t3 on t1.FrequencyDetailId  = t3.Id
	where t1.Id in
	(
		select distinct pp.Id from [Role] r 
		join [Plan] p on p.RoleId= r.id
		join [PlanMeasure] pm on pm.PlanId = p.Id
		join PayoutPercentage pp on pp.Id = pm.PayoutPercentageId
		where r.CountryId = @CountryId
	) 
end		
END
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_GetMeasures]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_GetMeasures] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Proc_GetMeasures]
(
@CountryId int
)
AS
BEGIN
IF(@CountryId >0)
begin
select * from 
(
	select c.Name as Country,
	bu.Name as BusinessUnit,
	r.Name as Role, 
	t1.[Sequence] as MeasureSequence, 
	t1.MeasureName as Measure, 
	f.Name as Frequency, 
	t1.MeasureWeightage, 
	pc.Name as PayoutCurve, 
	pt.Name as PayoutType, 
	pp.Name as IncentivePayout,
	'No' as [Modifier],
	(Case When t1.HasGoal = 0 Then 'No'
	     Else  'Yes' END) as [HasGoal],
	t1.DataType as [KPI /Modifier Name],
	t1.ValueType as [KPI/Modifier Value],
	(Case When t1.IsKPI = 0 Then 'No'
	     Else  'Yes' END) as [IsKPI]
	from PlanMeasure t1
	Join [Plan] p on p.Id = t1.PlanID
	Join [Role] r on r.ID = p.RoleID
	Join Country c on c.ID = r.CountryID
	Join BusinessUnit bu on bu.ID = r.BusinessUnitId
	Join Frequency f on F.ID = t1.FrequencyId
	Join PayoutCurve pc on pc.ID = t1.PayoutCurveId
	Join PayoutPercentage pp on pp.ID = t1.PayoutPercentageId
	Join PayoutType pt on pt.ID = t1.PayoutTypeId
	where c.Id = @CountryId
	Union ALL
	select 
	c.Name as Country,
	bu.Name as BusinessUnit,
	r.Name as Role, 
	4 as MeasureSequence, 
	t1.ModifierName as Measure, 
	'Annual' as Frequency, 
	NULL AS MeasureWeightage, 
	pc.Name as PayoutCurve, 
	'Discrete' as PayoutType, 
	'Annual' as IncentivePayout,
	'YES' as [Modifier],
     'No' as [HasGoal],
	t1.DataType  as [KPI/Modifier Value],
	t1.ValueType as [KPI/Modifier Value],
	'No' as [IsKPI]
	from PlanModifier t1
	Join [Plan] p on p.Id = t1.PlanID
	Join [Role] r on r.ID = p.RoleID
	Join Country c on c.ID = r.CountryID
	Join BusinessUnit bu on bu.ID = r.BusinessUnitId
	Join PayoutCurve pc on pc.ID = t1.PayoutCurveId
	where c.Id = @CountryId
) r
order by r.Country, r.BusinessUnit, r.Role, [Modifier], r.MeasureSequence
end
ELSE
begin 
select * from 
(
	select c.Name as Country,
	bu.Name as BusinessUnit,
	r.Name as Role, 
	t1.[Sequence] as MeasureSequence, 
	t1.MeasureName as Measure, 
	f.Name as Frequency, 
	t1.MeasureWeightage, 
	pc.Name as PayoutCurve, 
	pt.Name as PayoutType, 
	pp.Name as IncentivePayout,
	'No' as [Modifier],
	(Case When t1.HasGoal = 0 Then 'No'
	     Else  'Yes' END) as [HasGoal],
	t1.DataType as [KPI /Modifier Name],
	t1.ValueType as [KPI/Modifier Value],
	(Case When t1.IsKPI = 0 Then 'No'
	     Else  'Yes' END) as [IsKPI]
	
	from PlanMeasure t1
	Join [Plan] p on p.Id = t1.PlanID
	Join [Role] r on r.ID = p.RoleID
	Join Country c on c.ID = r.CountryID
	Join BusinessUnit bu on bu.ID = r.BusinessUnitId
	Join Frequency f on F.ID = t1.FrequencyId
	Join PayoutCurve pc on pc.ID = t1.PayoutCurveId
	Join PayoutPercentage pp on pp.ID = t1.PayoutPercentageId
	Join PayoutType pt on pt.ID = t1.PayoutTypeId
	Union ALL
	select 
	c.Name as Country,
	bu.Name as BusinessUnit,
	r.Name as Role, 
	4 as MeasureSequence, 
	t1.ModifierName as Measure, 
	'Annual' as Frequency, 
	NULL AS MeasureWeightage, 
	pc.Name as PayoutCurve, 
	'Discrete' as PayoutType, 
	'Annual' as IncentivePayout,
	'No' as [Modifier],
	'No'as [HasGoal],
	t1.DataType  as [KPI/Modifier Value],
	t1.ValueType as [KPI/Modifier Value],
	'No'as [IsKPI]
	from PlanModifier t1
	Join [Plan] p on p.Id = t1.PlanID
	Join [Role] r on r.ID = p.RoleID
	Join Country c on c.ID = r.CountryID
	Join BusinessUnit bu on bu.ID = r.BusinessUnitId
	Join PayoutCurve pc on pc.ID = t1.PayoutCurveId
) r
order by r.Country, r.BusinessUnit, r.Role, [Modifier], r.MeasureSequence
end
END
GO
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_GetRoles]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_GetRoles] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Proc_GetRoles]
( 
@CountryId int
)
AS
BEGIN
IF(@CountryId = 0)
begin
SELECT * From
(
	select C.Name as Country ,
	Bu.Name as BusinessUnit,
	R.Name as Role,
	p.TargetIncentivePercentage AS RoleTargetIncentive
	from  Role R 
	inner join [Plan] p on p.RoleId =R.Id
	inner join Country C on C.Id=R.CountryId
	inner join BusinessUnit Bu on Bu.Id=R.BusinessUnitId
)
rol
ORDER BY rol.Country,rol.BusinessUnit,rol.Role
end
ELSE
begin
SELECT * From
(
	select C.Name as Country ,
	Bu.Name as BusinessUnit,
	R.Name as Role,
	p.TargetIncentivePercentage AS RoleTargetIncentive
	from  Role R 
	inner join [Plan] p on p.RoleId =R.Id
	inner join Country C on C.Id=R.CountryId
	inner join BusinessUnit Bu on Bu.Id=R.BusinessUnitId where c.Id = @CountryId
)
rol
ORDER BY rol.Country,rol.BusinessUnit,rol.Role
end
END
GO
-------------------------------------------------------------------------------------------------------------------------------------------------- 
IF OBJECT_ID('[dbo].[Proc_ImportSalary]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_ImportSalary] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Proc_ImportSalary] 
(
	@Salary As [dbo].[TYPE_SALARY_IMPORT] readonly
)
AS
BEGIN

;merge UserSalaryDetail t using 
(
	Select u.Id,StartDate as StartDate,Salary as Salary from @Salary s
	Join [User] u on u.LoginID = s.UserID
	
) s on t.UserId = s.Id
When  matched and t.StartDate <> s.StartDate and t.EndDate is null THEN
Update  set t.EndDate =  CASE WHEN t.StartDate > s.StartDate THEN t.StartDate ELSE DATEADD(day,dateDiff(day,1,s.StartDate),0) END 
OUTPUT $action,INSERTED.*;


;merge UserSalaryDetail t using 
(
	Select u.Id, StartDate as StartDate, EndDate, Salary as Salary from @Salary s
	Join [User] u on u.LoginID = s.UserID
	
) s on t.UserId = s.Id and t.StartDate = s.StartDate
WHEN NOT Matched then
INSERT (UserId,StartDate,Salary) 
values(s.Id, s.StartDate, s.Salary)
When  matched THEN
Update  set t.EndDate =  s.EndDate, t.Salary = s.Salary
OUTPUT $action,INSERTED.*;
END
Go
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_ImportUserPayoutHistory]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_ImportUserPayoutHistory] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter Proc [dbo].[Proc_ImportUserPayoutHistory](@DataImportPayoutHistory [TYPE_DATA_IMPORT_PAYOUTHISTORY] Readonly)
AS
BEGIN
select U.Id as UserID,
	r.Year, r.[Plan], r.MeasureName, 
	FrequencyId = f.Id,
	Q1= r.q1 ,
	Q2= r.q2 , 
	Q3= r.q3 , 
	Q4= r.q4 ,
	r.UserADID
	into #temp1465
from
(
	select  cast([Year]as int) AS [Year],[Plan], UserADID, MeasureName, Freq, Q1, Q2, Q3, Q4
	from @DataImportPayoutHistory

	--union all
	--select [Year],EmpCode,[Plan],UserADID,MeasureName2,
 --Freq2 ,Q12 ,Q22 ,Q32 ,Q42 
	--from @DataImportPayoutHistory
	--where MeasureName2 is not null and MeasureName2<>''
	--union all
	--select [Year],EmpCode,[Plan],UserADID,MeasureName3,
	--Freq3, Q13 , Q23, Q33, Q43
	--from @DataImportPayoutHistory
	--where MeasureName3 is not null and MeasureName3<>''
	--union all
	--select [Year],EmpCode,[Plan],UserADID,MeasureName4,
	-- Freq4 ,Q14  ,Q24 ,Q34 ,Q44 
	--from @DataImportPayoutHistory						
	--where MeasureName4 is not null and MeasureName4<>''
) r
Join [User] u on u.LoginID=r.UserADID
join [Frequency] f on f.Name=(Case 
		When  Freq='Q' Then 'Quarterly' 
		WHEN Freq='S'THEN  'HalfYearly'
		WHEN Freq ='A'THEN 'Annual' END)
Where 
r.MeasureName <> '' and r.MeasureName <> 'N/A' and r.MeasureName is not null


;MERGE UserPayoutHistory p
using
(
		Select h.[UserId],h.FrequencyId,h.[Year] ,h.[Plan],h.[MeasureName],h.Q1,h.Q2,h.Q3,h.Q4 from #temp1465 h
		

)s on s.[Plan]=p.[PlanName] and s.[UserId]=p.[UserId] and s.[MeasureName]=p.[MeasureName] and s.[Year]=p.[Year]
when not matched THEN
insert(UserId,[Year],PlanName,MeasureName,FrequencyId,Quarter1,Quarter2,Quarter3,Quarter4)values(s.UserId,s.[Year],s.[Plan],s.MeasureName,s.FrequencyId,s.Q1,s.Q2,s.Q3,s.Q4)
when matched and (p.MeasureName<>s.MeasureName or p.FrequencyId<>s.FrequencyId or p.Quarter1<>s.Q1 or p.Quarter2<>s.Q2 or p.Quarter3<>s.Q3 or p.Quarter4<>s.Q4) Then
UPDATE SET p.MeasureName = s.MeasureName , p.FrequencyId=s.FrequencyId , p.Quarter1 = s.Q1 , p.Quarter2=s.Q2 , p.Quarter3=s.Q3 , p.Quarter4=s.Q4

OUTPUT $action,INSERTED.*;
END
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_ImportUserDataWithRole]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_ImportUserDataWithRole] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter Proc [dbo].[Proc_ImportUserDataWithRole](@UserDataImport [TYPE_USERDATAIMPORT] Readonly)
AS
BEGIN
;merge [User] u
using 
(
     select c.Id as CountryID,bu.Id as BusinessUnitId,t.UserADID,t.UserName,t.EmployeeID,c.LanguageID from @UserDataImport t
	 join Country c on c.Name=t.Country
	 join BusinessUnit bu on bu.Name=t.BusinessUnit
	 where (t.UserADID is not null and t.userADID !='')
)s on s.UserADID = u.LoginID
when not matched then
insert (LoginID,UserName,CountryId,BusinessUnitId,CityId,SaveSimulation,LanguageId,LastLogin,IsActive,EmployeeID)values(s.UserADID,s.UserName,s.CountryID,s.BusinessUnitId,null,1,s.LanguageID,null,1,s.EmployeeID)
output $action, INSERTED.*;


;merge  UserRoleDetail ur
using 
(
    select r.Id as RoleId,u.Id as UserId from @UserDataImport t
	join [User] u on u.LoginID=t.UserADID
	Join [Country] c on c.Name=t.Country
	join [BusinessUnit] bu on bu.Name=t.BusinessUnit
	join [Role] r on r.[Name]=t.[Role] and r.CountryId = c.Id and r.BusinessUnitId = bu.Id
)s on s.UserId=ur.UserId and ur.RoleId=s.RoleId
when not matched then 
insert (UserId,RoleId,StartDate,EndDate) values(s.UserId,s.RoleId,'2018-01-01','2018-12-31')
output $action, INSERTED.*;
--;merge UserSalaryDetail us

--using
--(
--    select Id, StartDate = CAST('2018-01-01' as DATE), EndDate = CAST('2018-03-31' as DATE), Salary=10000 from [User]
--    UNION ALL
--    select Id, StartDate = CAST('2018-04-01' as DATE), EndDate = CAST('2018-12-31' as DATE),Salary=12000 from [User]
--)s on s.Id = us.UserId and s.StartDate=us.StartDate
--when not matched THEN
--insert(UserId,StartDate,EndDate,Salary)values(s.Id,s.StartDate,s.EndDate,s.Salary)
--output $action,INSERTED.*;
END
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_DataUploadingCodeForRemovingData]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_DataUploadingCodeForRemovingData] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter Proc [dbo].[Proc_DataUploadingCodeForRemovingData]
AS
BEGIN

DELETE from USerSimulationModifierDetail
DBCC CHECKIDENT ('USerSimulationModifierDetail', RESEED, 0)

delete from UserSimulationMeasureFrequencyDetail
DBCC CHECKIDENT ('UserSimulationMeasureFrequencyDetail', RESEED, 0)

delete from UserSimulationMeasureDetail
DBCC CHECKIDENT ('UserSimulationMeasureDetail', RESEED, 0)

delete from UserSimulation
DBCC CHECKIDENT ('UserSimulation', RESEED, 0)

delete from UserTargetDetail
DBCC CHECKIDENT ('UserTargetDetail', RESEED, 0)

delete from UserTarget
DBCC CHECKIDENT ('UserTarget', RESEED, 0)

delete from PlanModifier
DBCC CHECKIDENT ('PlanModifier', RESEED, 0)

delete from PlanMeasure
DBCC CHECKIDENT ('PlanMeasure', RESEED, 0)

delete from PayoutCurveDetail
DBCC CHECKIDENT ('PayoutCurveDetail', RESEED, 0)


delete from PayoutCurve
DBCC CHECKIDENT ('PayoutCurve', RESEED, 0)


delete from PayoutPercentageDetail
DBCC CHECKIDENT ('PayoutPercentageDetail', RESEED, 0)


delete from PayoutPercentage
DBCC CHECKIDENT ('PayoutPercentage', RESEED, 0)


delete from PayoutCurveDetail
DBCC CHECKIDENT ('PayoutCurveDetail', RESEED, 0)


delete from UserSalaryDetail
DBCC CHECKIDENT ('UserSalaryDetail', RESEED, 0)


delete from UserRoleDetail
DBCC CHECKIDENT ('UserRoleDetail', RESEED, 0)

delete from [Plan]
DBCC CHECKIDENT ('plan', RESEED, 0)

delete from [Role]
DBCC CHECKIDENT ('Role', RESEED, 0)


END
GO
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_ImportGoalAmount]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_ImportGoalAmount] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Proc [dbo].[Proc_ImportGoalAmount](@tempGoalAmount [TYPE_DATA_IMPORT_GOALAMOUNT] Readonly)
AS
BEGIN 
SELECT u.Id as UserID,r.MeasureSequence, fd.Id as FrequemcyID,p.Id as PlanId, PM.ID as PlanMeasuereId,r.Annual,r.H1,r.H2,r.Q1,r.Q2,r.Q3,r.Q4
into #temp1
FROM
(
	select [Country],[BusinessUnit], [Role], UserADID, MeasureSequence,Annual, H1,H2,Q1,Q2,Q3,Q4
	from @tempGoalAmount
)r
inner join [user] u on u.LoginID = r.UserADID
inner join [Country] c on c.Name=r.Country
inner join [BusinessUnit] b on b.Name=r.BusinessUnit
inner join [Role] ro on ro.Name = r.[Role]	and ro.CountryId=c.Id and ro.BusinessUnitId=b.Id
inner join [Plan] p on p.RoleId = ro.Id
inner join [PlanMeasure] pm on pm.PlanId=p.Id and pm.Sequence=r.MeasureSequence
inner join [Frequency] fd  on fd.Id=pm.FrequencyId
;merge UserTarget ut
using 
(
    select Distinct t.UserID,t.PlanId  from #temp1 t
)s on s.UserId = ut.UserId and s.PlanId=ut.planId
when not matched then 
insert(UserId,PlanId)values(s.UserID,s.planId)
output $action,INSERTED .*;
merge UserTargetDetail ut
using
(
	select u.Id as UserTId,te.PlanMeasuereId,fd.Id as FrequencyDetailId,
	(case when fd.Id=1 Then (te.Annual)
	 when fd.Id=2 Then te.H1
	 WHEN fd.Id=3 Then te.H2
	 When fd.Id=4 Then te.Q1
	 when fd.Id=5 then te.Q2
	 when fd.Id=6 then te.Q3
	Else te.Q4  End) as GoalAmount
 from #temp1 te
	inner join UserTarget u on u.UserId=te.UserID and u.PlanId=te.PlanId
	inner join [FrequencyDetail] fd on fd.FrequencyId=te.FrequemcyID
	 
)s on s.UserTId=ut.UserTargetId and s.PlanMeasuereId=ut.PlanMeasureId and s.FrequencyDetailId=ut.FrequencyDetailId
when not matched then
insert(UserTargetId,PlanMeasureId,FrequencyDetailId,Incentive,Goal)values(s.UserTId,s.PlanMeasuereId,s.FrequencyDetailId,'0',s.GoalAmount)
when matched and (ut.Goal<>s.GoalAmount) Then
UPDATE SET ut.Goal=s.GoalAmount
OUTPUT $action, INSERTED.*;
END
Go
-------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_GetGoalAmount]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_GetGoalAmount] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Proc_GetGoalAmount]
(
	@CountryID int
)
AS
BEGIN

IF @CountryID < 1
BEGIN
	SET @CountryID = NULL;
END

--drop table #result
select * into #result from
(
select 
u.LoginID,u.EmployeeID,p.Year,c.Name as Country,b.Name as BusinessUnit,r.Name as [Role], t2.UserId, t3.PlanId, t1.PlanMeasureId, t3.[Sequence], t3.MeasureName, t3.FrequencyId,
FrequencyDetailId = (Case 
When t1.FrequencyDetailId = 1 Then 'Q4'
When t1.FrequencyDetailId = 2 Then 'Q2'
When t1.FrequencyDetailId = 3 Then 'Q4'
When t1.FrequencyDetailId = 4 Then 'Q1'
When t1.FrequencyDetailId = 5 Then 'Q2'
When t1.FrequencyDetailId = 6 Then 'Q3'
When t1.FrequencyDetailId = 7 Then 'Q4'
ELSE 'UN' END),
Goal 
from UserTargetDetail t1
Join UserTarget t2 on t2.Id = t1.UserTargetId
Join PlanMeasure t3 on t3.Id = t1.PlanMeasureId
join [User] u on u.Id=t2.UserId
join [UserRoleDetail] rd on rd.UserId=u.Id
join [Country] c on c.Id=u.CountryId
join BusinessUnit b on b.Id=u.BusinessUnitId
join [Role] r on r.Id = rd.RoleId
join [Plan] p on p.Id=r.Id 
where c.Id =ISNULL(@CountryID, c.Id)
) r
PIVOT  
(  
MAX(GOAL)FOR FrequencyDetailId IN ([Q1], [Q2], [Q3], [Q4])
) AS Tab2  

Select 
t1.EmployeeID as EmpCode,t1.Year,t1.Country,t1.BusinessUnit,t1.Role,t1.LoginID,
[Measure Sequence1] = t1.[Sequence], Q1_1 = t1.Q1, Q2_1 = t1.Q2, Q3_1 = t1.Q3, Q4_1 = t1.Q4,
[Measure Sequence2]= t2.[Sequence], Q1_2=t2.Q1, Q2_2 =t2.Q2,Q3_2= t2.Q3,Q4_2 = t2.Q4 ,
[Measure Sequence3]= t3.[Sequence], Q1_3=t3.Q1, Q2_3=t3.Q2, Q3_3=t3.Q3, Q4_3=t3.Q4
--[Measure Sequence4]= t4.[Sequence], Q1_4=t4.Q1, Q2_4=t4.Q2, Q3_4=t4.Q3, Q4_4=t4.Q4  
from #result t1  
 join #result t2 on t1.UserId = t2.UserId and t1.PlanId = t2.PlanId and t2.[Sequence]=2
left join #result t3 on t1.UserId = t3.UserId and t1.PlanId = t3.PlanId and t3.[Sequence]=3
--full join #result t4 on t1.UserId = t4.UserId and t1.PlanId = t3.PlanId
Where t1.[Sequence] = 1
-- and t2.[Sequence] = 2 and t3.[Sequence] = 3  
--and t4.[Sequence]=4

END

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_GetPayoutHistory]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_GetPayoutHistory] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE [SIPMiniCalculator]
GO
/****** Object:  StoredProcedure [dbo].[Proc_GetPayoutHistory]    Script Date: 12/20/2018 2:39:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Proc_GetPayoutHistory]
(
	@CountryID int
	--@Year int 
) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
IF @CountryID < 1
BEGIN
	SET @CountryID = NULL;
END	

select 
t1.[Year],t2.LoginID, t2.EmployeeID,t1.PlanName, t1.MeasureName,t3.Name as Frequency,t1.Quarter1, t1.Quarter2 ,t1.Quarter3, t1.Quarter4
from [UserPayoutHistory] t1
Join [User] t2 on t2.Id = t1.UserId
join [FrequencyDetail] t4 on t4.Id=t1.FrequencyId
Join [Frequency] t3 on t3.Id = t4.FrequencyId
	--WHERE [Year]=@Year
 WHERE t2.CountryId = ISNULL(@CountryID, t2.CountryID)
 ORDER BY UserId
END

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_GetUserDownloadedDocuments]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_GetUserDownloadedDocuments] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROC [dbo].[Proc_GetUserDownloadedDocuments]      
(      
 @CountryId int,  
 @year int=NULL		  
)      
AS      
BEGIN      

DECLARE @currentYear AS int      
SET @currentYear=ISNULL(@year,YEAR(GETUTCDATE()))

select     
 u.Id,u.EmployeeID, u.UserName as [UserName], c.Name as Country,t.LastDownloadedDate as [Downloaded Date],    
 (case when t.[UserDocumentAccessCount]>0 then 'Yes' else  'No' end) as [Is User Document Uploaded],     
 (case when t.[UserDocumentAccessCount]>0 then 'Yes' else  'No' end)  as [Is User Document Accessed],    
 (case when t.[CountryDocumentAccessCount]>0 then 'Yes' else 'No' end)  as [Is Country Document Uploaded],  
  (case when t.[CountryDocumentAccessCount]>0 then 'Yes' else 'No' end)  as [Is Country Document Accessed]     
 from [user] u inner join Country c on u.CountryId=c.Id      
 left join      
 (  
 select  UserId,  [Year], LastDownloadedDate,[1] as [UserDocumentAccessCount],[2] as [CountryDocumentAccessCount]  
 from  
 (select UserId, [Year],LastDownloadedDate, DocumentTypeId from TrackDocumentDownload 
  where (DocumentName is not null and DocumentName <>'') 
         and [Year]= @currentYear) as s  
 pivot  
 ( count(DocumentTypeId) for DocumentTypeId in ([1],[2])  
 ) as pt  
 )t on u.Id=t.UserId      
  Where u.CountryId = (case @CountryId when 0 then u.CountryId else @CountryId end)      
          
END  
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Proc_GetUserSalary]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_GetUserSalary] AS Select 1')
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[Proc_GetUserSalary]
(
	@CountryID int
)
AS
BEGIN
IF @CountryID < 1
BEGIN
	SET @CountryID = NULL;
END

select u.LoginID,u.UserName,u.EmployeeID,s.Salary,u.LastLogin,c.Name 
from [dbo].[User] u 
join [dbo].[UserSalaryDetail] s on s.userId = u.Id
left outer join [dbo].[UserSalaryDetail] s1 on (u.Id = s1.UserId AND (s.Id <s1.Id)) 
join [dbo].[Country] c on u.CountryId = c.Id
 Where s1.Id Is Null
 AND u.CountryID = ISNULL(@CountryID, u.CountryID)
 END
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 IF OBJECT_ID('[dbo].[Proc_DocumentAccessRecordInsert]', 'P') IS NULL
EXEC('CREATE PROCEDURE [dbo].[Proc_DocumentAccessRecordInsert] AS Select 1')
/****** Object:  StoredProcedure [dbo].[Proc_DocumentAccessRecordInsert]    Script Date: 12/17/2018 8:12:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[Proc_DocumentAccessRecordInsert]
(
	@UserId as int,
	@DocumentName as varchar(1000),
	@DocumentType as varchar(200),
	@Year as int=null
)
AS
BEGIN

 DECLARE @DocTypeId as int
 select @DocTypeId=id from DocumentType where replace([name],' ','')=@DocumentType

 insert into TrackDocumentDownload(UserId,DocumentName,DocumentTypeId,[Year],LastDownloadedDate)
        values(@userId,@DocumentName,@DocTypeId,@year,GETUTCDATE())

END
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------