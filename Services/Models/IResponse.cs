namespace Services.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Base interface for responses.
    /// </summary>
    public interface IResponse
    {
        string RawJson { get; set; }

        string RawQueryString { get; set; }

        Status? Status { get; set; }

        string ErrorMessage { get; set; }

        IEnumerable<string> HtmlAttributions { get; set; }
    }
}