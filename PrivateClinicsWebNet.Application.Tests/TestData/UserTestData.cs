using PrivateClinicsWebNet.Application.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Tests.TestData
{
    public class UserTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new RegisterDto("example@gmail.com", "password", "Doctor") };
            yield return new object[] { new RegisterDto("template@gmail.com", "passcode", "Patient") };
            yield return new object[] { new RegisterDto("email@gmail.com", "pincode", "Admin") };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}