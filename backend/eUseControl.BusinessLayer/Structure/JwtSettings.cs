namespace eUseControl.BusinessLayer.Structure;

public static class JwtSettings
{
    public const string Issuer = "ClinicQueueApi";
    public const string Audience = "ClinicQueueClients";
    public const string SecretKey = "clinicqueue_2026_super_secret_min_32_chars!";
    public const int ExpireMinutes = 60;
}
