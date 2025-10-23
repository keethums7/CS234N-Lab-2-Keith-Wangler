using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;

using MySql.Data.MySqlClient;

namespace MMABooksTests
{
    [TestFixture]
    public class StateDBTests
    {
        private State state;

        [SetUp]
        public void SetUp()
        {
            // testing with Alabama, reset any changes
            state = new State("AL", "Alabama");
        }

        [Test]
        public void TestGetStates()
        {
            List<State> states = StateDB.GetStates();
            Assert.AreEqual(53, states.Count);
            Assert.AreEqual("Alabama", states[0].StateName);
        }

        [Test]
        public void TestGetStatesDBUnavailable()
        {
            Assert.Throws<MySqlException>(() => StateDB.GetStates());
        }
    }
}
