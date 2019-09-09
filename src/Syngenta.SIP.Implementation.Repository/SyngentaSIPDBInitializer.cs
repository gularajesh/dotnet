// ***********************************************************************
// <copyright file="SyngentaSIPDBInitializer.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Repository
{
    using System.Data.Entity;

    /// <summary>
    /// SyngentaSIPDBInitializer class
    /// </summary>
    /// <seealso cref="System.Data.Entity.CreateDatabaseIfNotExists{Syngenta.SIP.Implementation.Repository.SyngentaSIPContext}" />
    public class SyngentaSIPDBInitializer : CreateDatabaseIfNotExists<SyngentaSIPContext>
    {
        /// <summary>
        /// A method that should be overridden to actually add data to the context for seeding.
        /// The default implementation does nothing.
        /// </summary>
        /// <param name="context">The context to seed.</param>
        protected override void Seed(SyngentaSIPContext context)
        {
            base.Seed(context);
        }
    }
}
