namespace BuildIndicatorCommon.Model
{
    public class Build
    {
        public enum BuildState
        {
            Unknown,
            Success,
            Unstable,
            Failure,
            Aborted
        }

        public string BuildNumber { get; set; }
        public BuildState State { get; set; }
        public bool IsBuilding { get; set; }
        public string Link { get; set; }
        public string ApiLink
        {
            get { return Link + "/api/xml"; }
        }
    }
}
