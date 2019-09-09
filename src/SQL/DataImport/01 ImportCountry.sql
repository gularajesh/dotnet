declare @DataImportCountry  as [TYPE_COUNTRY_IMPORT]
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','Australasia','Australia','EN-US','AUD')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','South Asia','Bangladesh','EN-US','BDT')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','China','China','CHINESE','CNY')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','ASEAN','Indonesia','BAHASA','IDR')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','North East Asia','Japan','JAPANESE','JPY')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','ASEAN','Malaysia','EN-US','MYR')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','Australasia','New Zealand','EN-US','NZD')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','South Asia','Pakistan','EN-US','PKR')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','ASEAN','Philippines','EN-US','PHP')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','North East Asia','South Korea','EN-US','KRW')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','North East Asia','Taiwan','EN-US','TWD')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','ASEAN','Thailand','THAI','THB')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','ASEAN','Vietnam','VIETNAMESE','VND')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','Asia Pacific Other','Singapore','EN-US','SGD')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','South Asia','India','EN-US','INR')
insert into @DataImportCountry(RegionName,TerritoryName,CountryName,DefaultLanguange,Currency)values('APAC','ASEAN','Myanmar','EN-US','MMK')




update @DataImportCountry set RegionName = LTRIM(RTRIM(RegionName))
update @DataImportCountry set TerritoryName = LTRIM(RTRIM(TerritoryName))
update @DataImportCountry set CountryName = LTRIM(RTRIM(CountryName))
update @DataImportCountry set DefaultLanguange = LTRIM(RTRIM(DefaultLanguange))
update @DataImportCountry set Currency = LTRIM(RTRIM(Currency))
exec Proc_ImportCountry @DataImportCountry