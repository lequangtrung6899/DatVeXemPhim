using System.Security.Cryptography;

namespace DatVeXemPhim.Services;

// Băm & xác thực mật khẩu bằng PBKDF2-HMACSHA256 (Rfc2898DeriveBytes có sẵn trong .NET —
// không cần thêm gói NuGet nào). Salt ngẫu nhiên cho mỗi mật khẩu, số vòng lặp lưu kèm
// trong chuỗi hash để có thể tăng độ khó về sau mà không phá vỡ các hash cũ.
// Định dạng lưu trong DB: "{iterations}.{saltBase64}.{keyBase64}"
public static class PasswordHasherHelper
{
    private const int SaltSize = 16;       // 128-bit
    private const int KeySize = 32;        // 256-bit
    private const int Iterations = 100_000;

    public static string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
        return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
    }

    // So khớp mật khẩu người dùng nhập với hash đã lưu. Trả về false (không throw) cho
    // mọi dữ liệu không đúng định dạng, kể cả các bản ghi demo cũ trước khi có hashing thật.
    public static bool Verify(string? storedHash, string password)
    {
        if (string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(password)) return false;

        var parts = storedHash.Split('.', 3);
        if (parts.Length != 3) return false;
        if (!int.TryParse(parts[0], out var iterations) || iterations <= 0) return false;

        byte[] salt, expectedKey;
        try
        {
            salt = Convert.FromBase64String(parts[1]);
            expectedKey = Convert.FromBase64String(parts[2]);
        }
        catch (FormatException)
        {
            return false;
        }

        var actualKey = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expectedKey.Length);
        return CryptographicOperations.FixedTimeEquals(actualKey, expectedKey);
    }
}
