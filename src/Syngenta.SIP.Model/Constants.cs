// ***********************************************************************
// <copyright file="Constants.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************

namespace Syngenta.SIP.Models
{
    using System;

    /// <summary>
    /// class Constants
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Gets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        public static string Success
        {
            get { return "Success"; }
        }

        /// <summary>
        /// Gets the removecolumn.
        /// </summary>
        /// <value>
        /// The removecolumn.
        /// </value>
        public static string Removecolumn
        {
            get { return "SalaryInDB"; }
        }

        /// <summary>
        /// Gets the removecolumn gaol.
        /// </summary>
        /// <value>
        /// The removecolumn gaol.
        /// </value>
        public static string RemovecolumnGaol
        {
            get { return "Goal"; }
        }

        /// <summary>
        /// class SimulationCodes
        /// </summary>
        public class SimulationCodes
        {
            /// <summary>
            /// Gets the simulations maximum count reached.
            /// </summary>
            /// <value>
            /// The simulations maximum count reached.
            /// </value>
            public static string SimulationsMaximumCountReached
            {
                get { return "SimulationsMaximumCountReached"; }
            }

            /// <summary>
            /// Gets the simulations name already exists.
            /// </summary>
            /// <value>
            /// The simulations name already exists.
            /// </value>
            public static string SimulationsNameAlreadyExists
            {
                get { return "SimulationsNameAlreadyExists"; }
            }

            /// <summary>
            /// Gets the simulation deleted.
            /// </summary>
            /// <value>
            /// The simulation deleted.
            /// </value>
            public static string SimulationDeleted
            {
                get { return "SimulationDeleted"; }
            }

            /// <summary>
            /// Gets the simulations count.
            /// </summary>
            /// <value>
            /// The simulations count.
            /// </value>
            public static int SimulationsCount
            {
                get { return 3; }
            }
        }

        /// <summary>
        /// class ApplicationSettingCodes
        /// </summary>
        public class ApplicationSettingCodes
        {
            /// <summary>
            /// Gets the name of the application key.
            /// </summary>
            /// <value>
            /// The name of the application key.
            /// </value>
            public static string CryptoKey
            {
                get { return "CryptoKey"; }
            }
        }

        /// <summary>
        /// BlobSetting Class
        /// </summary>
        public class BlobSetting
        {
            /// <summary>
            /// Gets the name of the BLOB storage application setting.
            /// </summary>
            /// <value>
            /// The name of the BLOB storage application setting.
            /// </value>
            public static string BlobStorageAppSettingName
            {
                get { return "azure:storageConnectionString"; }
            }

            /// <summary>
            /// Gets the name of the storage container.
            /// </summary>
            /// <value>
            /// The name of the storage container.
            /// </value>
            public static string StorageContainerName
            {
                get { return "documents"; }
            }

            /// <summary>
            /// Gets the user storage container.
            /// </summary>
            /// <value>
            /// The user storage container.
            /// </value>
            public static string UserStorageContainer
            {
                get { return "userdocument"; }
            }

            /// <summary>
            /// Gets the country storage container.
            /// </summary>
            /// <value>
            /// The country storage container.
            /// </value>
            public static string CountryStorageContainer
            {
                get { return "countrydocument"; }
            }
            
        }

        /// <summary>
        /// Gets the current year.
        /// </summary>
        /// <value>
        /// The current year.
        /// </value>
        public static int CurrentYear
        {
            get { return DateTime.UtcNow.Year; }
        }
    }
}
