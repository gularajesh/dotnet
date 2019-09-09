// ***********************************************************************
// <copyright file="MappingConfig.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Repository
{
    using System.Data.Entity;
    using Syngenta.SIP.Implementation.Repository.Maps;

    /// <summary>
    /// MappingConfig class
    /// </summary>
    internal static class MappingConfig
    {
        /// <summary>
        /// Builds the catalogue context mappings.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void BuildCatalogueContextMappings(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BusinessUnitMap());
            modelBuilder.Configurations.Add(new RegionMap());
            modelBuilder.Configurations.Add(new TerritoryMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new LanguageMap());
            modelBuilder.Configurations.Add(new PlanMap());
            modelBuilder.Configurations.Add(new FrequencyMap());
            modelBuilder.Configurations.Add(new FrequencyDetialMap());
            modelBuilder.Configurations.Add(new PayoutCurveMap());
            modelBuilder.Configurations.Add(new PayoutCurveDetailMap());
            modelBuilder.Configurations.Add(new PayoutPercentageMap());
            modelBuilder.Configurations.Add(new PayoutPercentageDetailMap());
            modelBuilder.Configurations.Add(new PayoutTypeMap());
            modelBuilder.Configurations.Add(new PlanMeasure());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserRoleDetailMap());
            modelBuilder.Configurations.Add(new UserSalaryDetailMap());
            modelBuilder.Configurations.Add(new UserTargetMap());
            modelBuilder.Configurations.Add(new UserTargetDetailMap());
            modelBuilder.Configurations.Add(new UserSimulationMap());
            modelBuilder.Configurations.Add(new UserSimulationMeasureDetailMap());
            modelBuilder.Configurations.Add(new UserSimulationMeasureFrequencyDetailModelMap());
            modelBuilder.Configurations.Add(new ModifierMap());
            modelBuilder.Configurations.Add(new PlanModifierMap());
            modelBuilder.Configurations.Add(new UserSimulationModifierDetailMap());
            modelBuilder.Configurations.Add(new UserPayoutHistoryMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new UserPermissionMap());
        }

        /// <summary>
        /// Builds the catalogue security mappings.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void BuildCatalogueSecurityMappings(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationSettingMap());
        }
    }
}
