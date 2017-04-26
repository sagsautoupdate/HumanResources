namespace HumanResources.Utilities
{
    public enum UpdateStatuses
    {
        NoUpdateAvailable,
        UpdateAvailable,
        UpdateRequired,
        NotDeployedViaClickOnce,
        DeploymentDownloadException,
        InvalidDeploymentException,
        InvalidOperationException
    }
}