//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

//namespace DI.Domain.Services
//{
//    public class DbErrorResolver : StateManager
//    {
//        public DbErrorResolver(StateManagerDependencies dependencies) : base(dependencies)
//        {
//        }

//        public override int SaveChanges(bool acceptAllChangesOnSuccess)
//        {
//            try
//            {
//                return base.SaveChanges(acceptAllChangesOnSuccess);
//            }
//            catch (DbUpdateException ex)
//            {
//                throw Translate(ex);
//            }
//        }

//        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
//            CancellationToken cancellationToken = new CancellationToken())
//        {
//            try
//            {
//                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
//            }
//            catch (DbUpdateException ex)
//            {
//                throw Translate(ex);
//            }
//        }

//        private static Exception Translate(DbUpdateException ex)
//        {
//            var pgException = ex.GetBaseException() as sqlser;
//            if (pgException == null)
//                return ex;

//            return pgException.SqlState switch
//            {
//                PostgresErrorCodes.StringDataRightTruncation => new MaxLengthException(
//                    "Maximum length exceeded", ex.InnerException),
//                PostgresErrorCodes.NumericValueOutOfRange => new NumericOverflowException("Numeric overflow",
//                    ex.InnerException),
//                PostgresErrorCodes.NotNullViolation => new InsertNullException("Cannot insert null",
//                    ex.InnerException),
//                PostgresErrorCodes.UniqueViolation => new UniqueConstraintException("Unique constraint violation",
//                    ex.InnerException),
//                PostgresErrorCodes.ForeignKeyViolation => new ReferenceConstraintException(
//                    "Reference constraint violation", ex.InnerException),
//                _ => pgException
//            };
//        }
//    }
//}

