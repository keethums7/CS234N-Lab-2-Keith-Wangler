using System;
using System.Collections.Generic;
using System.Text;

namespace MMABooksBusinessClasses
{
    public class State
    {
        // there are several warnings in this file related to nullable properties and return values.
        // you can ignore them
        public State() { }

        public State(string code, string name)
        {
            StateCode = code;
            StateName = name;
        }



        private string stateCode;
        private string stateName;

        public string StateName {
            get
            {
                return stateName;
            }
            set
            {
                if (value.Trim().Length > 0 && value.Trim().Length <= 20)
                {
                    stateName = value;
                } 
                else
                {
                    throw new ArgumentOutOfRangeException("State name must be a string between 1-20 characters in length.");
                }
            }
        }

        public string StateCode { 
            get
            {
                return stateCode;
            }
            set
            {
                // this would normally be == 2 but there's some bad data in the database
                // I didn't realize that until I wrote the test for GetStates in StateDB
                if (value.Length <= 2)
                {
                    stateCode = value.ToUpper();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The state code must be exactly 2 characters.");
                }
            }
        }

        public override string ToString()
        {
            return StateCode + ", " + StateName;
        }
    }
}
