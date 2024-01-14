using ecommerce.Domain.Aggregates.UserAggregate.Events;
using ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.UserAggregate;
using ecommerce.Domain.Common.ValueObjects;
using ecommerce.DomainUnitTest.Aggregates.UserAggregate.Utilities;
using ecommerce.DomainUnitTest.Common.Utilities;

namespace ecommerce.DomainUnitTest.Aggregates.UserAggregate
{
    public partial class UserTest
    {
        public class UserAddressTest
        {
            #region Add Address
            [Fact]
            public void AddAddress_WhenAddressIsNull_ShouldThrowException()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;

                // Act
                var result = Record.Exception(() =>
                {
                    user.AddAddress(null!);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void AddAddress_WhenAddressExistsInList_ShouldReturnFalseAndDoesNotAddToListAndNotAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                user.AddAddress(new UserAddress(UserAddressTestUtility.ValidTitle + "x", AddressTestUtility.ValidAddress));
                user.ClearDomainEvents();

                var address = user.Addresses[0];

                // Act
                var result = user.AddAddress(address);

                // Assert
                Assert.False(result);
                Assert.False((bool)(user.Addresses.GroupBy(x => x.Title).Any(x => x.Count() > 1)));
                Assert.DoesNotContain(typeof(UserAddressAdded), user.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void AddAddress_WhenAddressDoesNotExistInList_ShouldReturnTrueAndAddToListAndAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;

                var address = new UserAddress(user.Addresses[0].Title + "a", user.Addresses[0].Address);

                // Act
                var result = user.AddAddress(address);

                // Assert
                Assert.True(result);
                Assert.Contains(address, user.Addresses);
                Assert.Contains(typeof(UserAddressAdded), user.DomainEvents.Select(x => x.GetType()));
            }
            #endregion

            #region Update Address
            [Fact]
            public void UpdateAddress_WhenAddressIsNull_ShouldThrowException()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;

                var addressTitle = UserAddressTestUtility.ValidTitle;

                // Act
                var result = Record.Exception(() =>
                {
                    user.UpdateAddress(addressTitle, null!);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdateAddress_WhenAddressDoesNotExitInList_ShouldThrowException()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                user.AddAddress(new UserAddress(UserAddressTestUtility.ValidTitle + "x", AddressTestUtility.ValidAddress));
                user.ClearDomainEvents();

                var addressTitle = user.Addresses[0].Title + "a";
                var address = new UserAddress(addressTitle, user.Addresses[0].Address);

                // Act
                var result = Record.Exception(() =>
                {
                    user.UpdateAddress(addressTitle, address);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdateAddress_WhenThereIsNoChangeInAddress_ShouldReturnFalseAndNotAddAsNewAddressAndNotAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                user.AddAddress(new UserAddress(UserAddressTestUtility.ValidTitle + "x", AddressTestUtility.ValidAddress));
                user.ClearDomainEvents();

                var address = user.Addresses[0];

                // Act
                var result = user.UpdateAddress(address.Title, address);

                // Assert
                Assert.False(result);
                Assert.False((bool)(user.Addresses.GroupBy(x => x.Title).Any(x => x.Count() > 1)));
                Assert.DoesNotContain(typeof(UserAddressUpdated), user.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdateAddress_WhenAddressTitleAlreadyExists_ShouldThrowException()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                user.AddAddress(new UserAddress(UserAddressTestUtility.ValidTitle + "x", AddressTestUtility.ValidAddress));
                user.ClearDomainEvents();

                var addressTitle = user.Addresses[0].Title;
                var address = new UserAddress(user.Addresses[0].Title + "x", user.Addresses[0].Address);

                // Act
                var result = Record.Exception(() =>
                {
                    user.UpdateAddress(addressTitle, address);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void UpdateAddress_WhenOnlyAddressTitleIsDifferent_ShouldReturnTrueAndUpdateAddressAndAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                user.AddAddress(new UserAddress(UserAddressTestUtility.ValidTitle + "x", AddressTestUtility.ValidAddress));
                user.ClearDomainEvents();

                var addressTitle = user.Addresses[0].Title;
                var address = new UserAddress(user.Addresses[0].Title + "a", user.Addresses[0].Address);

                // Act
                var result = user.UpdateAddress(addressTitle, address);

                // Assert
                Assert.True(result);
                Assert.Equal(address, user.Addresses[0]);
                Assert.Equal(address.Address, user.Addresses[0].Address);
                Assert.Contains(typeof(UserAddressUpdated), user.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdateAddress_WhenOnlyAddressInfoIsDifferent_ShouldReturnTrueAndUpdateAddressAndAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                user.AddAddress(new UserAddress(UserAddressTestUtility.ValidTitle + "x", AddressTestUtility.ValidAddress));
                user.ClearDomainEvents();

                var addressTitle = user.Addresses[0].Title;
                var address = new UserAddress(user.Addresses[0].Title,
                    new Address(AddressTestUtility.ValidStreet + "a",
                        AddressTestUtility.ValidZipCode,
                        AddressTestUtility.ValidCity,
                        AddressTestUtility.ValidCountry));

                // Act
                var result = user.UpdateAddress(addressTitle, address);

                // Assert
                Assert.True(result);
                Assert.Equal(address, user.Addresses[0]);
                Assert.Equal(address.Address, user.Addresses[0].Address);
                Assert.Contains(typeof(UserAddressUpdated), user.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void UpdateAddress_WhenAddressIsCompletelyDifferent_ShouldReturnTrueAndUpdateAddressAndAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                user.AddAddress(new UserAddress(UserAddressTestUtility.ValidTitle + "x", AddressTestUtility.ValidAddress));
                user.ClearDomainEvents();

                var addressTitle = user.Addresses[0].Title;
                var address = new UserAddress(user.Addresses[0].Title + "a",
                    new Address(AddressTestUtility.ValidStreet + "a",
                        AddressTestUtility.ValidZipCode,
                        AddressTestUtility.ValidCity,
                        AddressTestUtility.ValidCountry));

                // Act
                var result = user.UpdateAddress(addressTitle, address);

                // Assert
                Assert.True(result);
                Assert.Equal(address, user.Addresses[0]);
                Assert.Equal(address.Address, user.Addresses[0].Address);
                Assert.Contains(typeof(UserAddressUpdated), user.DomainEvents.Select(x => x.GetType()));
            }
            #endregion

            #region Remove Address
            [Fact]
            public void RemoveAddress_WhenAddressIsNull_ShouldThrowException()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;

                // Act
                var result = Record.Exception(() =>
                {
                    user.RemoveAddress(null!);
                });

                // Assert
                Assert.NotNull(result);
            }

            [Fact]
            public void RemoveAddress_WhenAddressDoesNotExistInList_ShouldReturnFalseAndNotAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                user.AddAddress(new UserAddress(UserAddressTestUtility.ValidTitle + "x", AddressTestUtility.ValidAddress));
                user.ClearDomainEvents();

                var address = new UserAddress(user.Addresses[0].Title + "a",
                    user.Addresses[0].Address);

                // Act
                var result = user.RemoveAddress(address);

                // Assert
                Assert.False(result);
                Assert.DoesNotContain(typeof(UserAddressRemoved), user.DomainEvents.Select(x => x.GetType()));
            }

            [Fact]
            public void RemoveAddress_WhenAddressExistsInList_ShouldReturnTrueAndRemoveFromListAndAddDomainEvent()
            {
                // Arrange
                User user = UserTestUtility.ValidUser;
                user.AddAddress(new UserAddress(UserAddressTestUtility.ValidTitle + "x", AddressTestUtility.ValidAddress));
                user.ClearDomainEvents();

                var address = user.Addresses[0];

                // Act
                var result = user.RemoveAddress(address);

                // Assert
                Assert.True(result);
                Assert.DoesNotContain(address, user.Addresses);
                Assert.Contains(typeof(UserAddressRemoved), user.DomainEvents.Select(x => x.GetType()));
            }
            #endregion
        }
    }
}
