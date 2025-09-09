using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dotmim.Sync.Tests.UnitTests
{
    public class SetupColumnsTests
    {
        [Fact]
        public void SetupColumns_Add_WithoutExcludedColumns_Should_AddAllColumns()
        {
            SetupColumns columns = new SetupColumns();
            columns.AddRange("CustomerID", "CustomerName", "CustomerAddress");

            Assert.Equal(3, columns.Count);
            Assert.Equal(0, columns.ExcludedCollection.Count);
            Assert.Contains("CustomerID", columns.InnerCollection);
            Assert.Contains("CustomerName", columns.InnerCollection);
            Assert.Contains("CustomerAddress", columns.InnerCollection);
        }

        [Fact]
        public void SetupColumns_Exclude_WithoutAddedColumns_Should_ExcludeAllColumns()
        {
            SetupColumns columns = new SetupColumns();
            columns.ExcludeRange("CustomerID", "CustomerName", "CustomerAddress");

            Assert.Equal(0, columns.Count);
            Assert.Equal(3, columns.ExcludedCollection.Count);
            Assert.Contains("CustomerID", columns.ExcludedCollection);
            Assert.Contains("CustomerName", columns.ExcludedCollection);
            Assert.Contains("CustomerAddress", columns.ExcludedCollection);
        }

        [Fact]
        public void SetupColumns_Exclude_AfterAddColumns_Should_NotContainExcludedColumns()
        {
            SetupColumns columns = new SetupColumns();
            columns.AddRange("CustomerID", "CustomerName", "CustomerAddress");
            columns.ExcludeRange("CustomerName", "CustomerAddress");

            Assert.Equal(1, columns.Count);
            Assert.Contains("CustomerID", columns.InnerCollection);
            Assert.DoesNotContain("CustomerAddress", columns.InnerCollection);
            Assert.DoesNotContain("CustomerName", columns.InnerCollection);
            Assert.Equal(2, columns.ExcludedCollection.Count);
            Assert.Contains("CustomerName", columns.ExcludedCollection);
            Assert.Contains("CustomerAddress", columns.ExcludedCollection);
        }

        [Fact]
        public void SetupColumns_Add_AfterExcludeColumns_Should_ContainAllAddedColumns()
        {
            SetupColumns columns = new SetupColumns();
            columns.ExcludeRange("CustomerName", "CustomerAddress");
            columns.AddRange("CustomerID", "CustomerName", "CustomerAddress");

            Assert.Equal(3, columns.Count);
            Assert.Equal(0, columns.ExcludedCollection.Count);
            Assert.Contains("CustomerID", columns.InnerCollection);
            Assert.Contains("CustomerName", columns.InnerCollection);
            Assert.Contains("CustomerAddress", columns.InnerCollection);
        }

    }
}