using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Shinobi.Tests.MockHelpers;

public class DbContextMock
{
    public static TContext GetMock<TData, TContext>(List<TData> listData, Expression<Func<TContext, DbSet<TData>>> dbSetSelectionExpression) where TData : class where TContext : DbContext
    {
        var lstDataQueryable = listData.AsQueryable();
        var dbSetMock = new Mock<DbSet<TData>>();
        var dbContext = new Mock<TContext>();
        
        dbSetMock.As<IQueryable<TData>>().Setup(s => s.Provider).Returns(lstDataQueryable.Provider);
        dbSetMock.As<IQueryable<TData>>().Setup(s => s.Expression).Returns(lstDataQueryable.Expression);
        dbSetMock.As<IQueryable<TData>>().Setup(s => s.ElementType).Returns(lstDataQueryable.ElementType);
        dbSetMock.As<IQueryable<TData>>().Setup(s => s.GetEnumerator()).Returns(() => lstDataQueryable.GetEnumerator());
        dbSetMock.Setup(x => x.Add(It.IsAny<TData>())).Callback<TData>(listData.Add);
        dbSetMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<TData>>())).Callback<IEnumerable<TData>>(listData.AddRange);
        dbSetMock.Setup(x => x.Remove(It.IsAny<TData>())).Callback<TData>(t => listData.Remove(t));
        dbSetMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<TData>>())).Callback<IEnumerable<TData>>(ts =>
        {
            foreach (var t in ts) { listData.Remove(t); }
        });

        dbContext.Setup(dbSetSelectionExpression).Returns(dbSetMock.Object);

        return dbContext.Object;
    }
}