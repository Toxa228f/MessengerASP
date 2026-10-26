using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letter.Domain.Enums
{
    public enum ResponceCodes
    {
        Ok = 200,
        NullTable = 305,
        BoolError = 310,
        UserExist = 315,
        WrongPassOrEmail = 320,
        UnCatchedError = 601,
    }

    public enum PrivateChatRole
    {
        User = 0,
        Bot = 1
    }

}
