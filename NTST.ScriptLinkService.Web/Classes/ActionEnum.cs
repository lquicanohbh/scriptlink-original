using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTST.ScriptLinkService.Web
{
    public enum ActionEnum
    {
        IsAnyValue,
        IsEqualTo,
        IsNotEqualTo,
        IsOneOf,
        IsNotOneOf,
        IsLessThan,
        IsLessThanOrEqualTo,
        IsGreaterThan,
        IsGreaterThanOrEqualTo,
        IsBetween,
        IsNotBetween,
        StartsWith,
        DoesNotStartWith,
        IsLike,
        IsNotLike
    }
}