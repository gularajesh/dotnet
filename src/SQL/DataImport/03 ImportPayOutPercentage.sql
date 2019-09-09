Declare @tempIncentivePayout as TYPE_INCENTIVEPERCENTAGE_IMPORT

insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Semi Annual','H1','40')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Semi Annual','H2','60')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter','Q1','25')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter','Q2','25')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter','Q3','25')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter','Q4','25')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Annual','Annual','100')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Semi Annual 2','H1','25')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Semi Annual 2','H2','75')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 1','Q1','20')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 1','Q2','20')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 1','Q3','20')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 1','Q4','40')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 2','Q1','20')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 2','Q2','20')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 2','Q3','30')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 2','Q4','30')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 3','Q1','10')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 3','Q2','10')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 3','Q3','30')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 3','Q4','50')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 4','Q1','10')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 4','Q2','10')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 4','Q3','20')
insert into @tempIncentivePayout(PayoutPercentage,FrequencyDetails,Percentage)values('Quarter 4','Q4','60')



update @tempIncentivePayout set PayoutPercentage = LTRIM(RTRIM(PayoutPercentage))
update @tempIncentivePayout set FrequencyDetails = LTRIM(RTRIM(FrequencyDetails))
exec Proc_ImportIncentivePercentage  @tempIncentivePayout
