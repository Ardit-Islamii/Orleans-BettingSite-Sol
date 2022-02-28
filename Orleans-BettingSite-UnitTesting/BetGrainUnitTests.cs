using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Xunit;
using Orleans.TestingHost;
using Moq;
using Orleans.Core;
using Orleans.Runtime;
using Orleans_BettingSite.Grains;
using Orleans_BettingSite.States;
using Orleans_BettingSite_Common.GrainInterfaces;
using Orleans_BettingSite_UnitTesting.Helper;

namespace Orleans_BettingSite_UnitTesting
{
    public class BetGrainUnitTests
    {
        private readonly Mock<IPersistentState<AmountState>> _amountStateMock;
        private readonly AmountState _amountState = BetGrainHelper.AmountStateData();
        private readonly Mock<ILoggerFactory> _loggerFactoryMock;
        private readonly BetGrain _gut;
        public BetGrainUnitTests()
        {
            _amountStateMock = new Mock<IPersistentState<AmountState>>();
            _loggerFactoryMock = new Mock<ILoggerFactory>();
            _gut = new BetGrain(_amountStateMock.Object, _loggerFactoryMock.Object);
        }

        [Fact]
        public async Task GetBetAmountAsync_ValidState_AmountReturned()
        {
            //Arrange
            _amountStateMock.Setup(x => x.State).Returns(_amountState);

            //Act
            var result = await _gut.GetBetAmountAsync();

            //Assert
            Assert.Equal(_amountState.Amount, result);
        }
    }
}