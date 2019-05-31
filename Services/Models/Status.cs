namespace Services.Models
{
    using System.Runtime.Serialization;

    public enum Status
    {
        Undefined = 0,

        [EnumMember(Value = "OK")]
        Ok,

        [EnumMember(Value = "ZERO_RESULTS")]
        ZeroResults,

        [EnumMember(Value = "OVER_QUERY_LIMIT")]
        OverQueryLimit,

        [EnumMember(Value = "REQUEST_DENIED")]
        RequestDenied,

        [EnumMember(Value = "INVALID_REQUEST")]
        InvalidRequest,

        [EnumMember(Value = "MAX_ELEMENTS_EXCEEDED")]
        MaxElementsExceeded,

        [EnumMember(Value = "MAX_WAYPOINTS_EXCEEDED")]
        MaxWaypointsExceeded,

        [EnumMember(Value = "NOT_FOUND")]
        NotFound,

        [EnumMember(Value = "UNKNOWN_ERROR")]
        UnknownError,

        [EnumMember(Value = "HTTP_ERROR")]
        HttpError,

        [EnumMember(Value = "NO_API_KEY")]
        InvalidKey
    }
}
