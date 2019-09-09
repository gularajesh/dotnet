// ***********************************************************************
// <copyright file="SyngentaSIPSerureDBInitializer.cs" company="Syngenta">
//     Copyright ©  2016
// </copyright>
// <summary>
//  </summary>
// ***********************************************************************
namespace Syngenta.SIP.Implementation.Repository
{
    using System.Data.Entity;

    /// <summary>
    /// SyngentaSIPSerureDBInitializer class
    /// </summary>
    /// <seealso cref="System.Data.Entity.CreateDatabaseIfNotExists{Syngenta.SIP.Implementation.Repository.SyngentaSIPSecurityContext}" />
    public class SyngentaSIPSerureDBInitializer : CreateDatabaseIfNotExists<SyngentaSIPSecurityContext>
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(SyngentaSIPSecurityContext context)
        {
            base.Seed(context);
        }
    }
}
