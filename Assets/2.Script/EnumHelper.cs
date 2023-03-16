using System;
using System.Collections;
using System.Collections.Generic;

public static class EnumHelper
{
    public static int GetEnumMemberCnt(Type _enum)
    {
        return _enum.GetEnumValues().Length;
    }
}
