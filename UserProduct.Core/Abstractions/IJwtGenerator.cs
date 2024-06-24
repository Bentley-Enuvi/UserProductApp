using UserProduct.Domain.Entities;

namespace UserProduct.Core.Abstractions
{
    public interface IJwtGenerator
    {
        public string GenerateToken(User user);
    }
}
