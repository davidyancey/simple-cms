// Type: AutoMapper.IConfiguration
// Assembly: AutoMapper, Version=2.0.0.232, Culture=neutral, PublicKeyToken=be96cd2c38ef1005
// Assembly location: C:\Users\Dad\Documents\Visual Studio 2010\Projects\AgileDevDays\AgileDevDays\packages\AutoMapper.2.0.0\lib\net40-client\AutoMapper.dll

using System;

namespace AutoMapper
{
    public interface IConfiguration : IProfileExpression, IFormatterExpression, IMappingOptions
    {
        IProfileExpression CreateProfile(string profileName);
        void CreateProfile(string profileName, Action<IProfileExpression> initializationExpression);
        void AddProfile(Profile profile);
        void AddProfile<TProfile>() where TProfile : new(), Profile;
        void ConstructServicesUsing(Func<Type, object> constructor);
        void Seal();
    }
}
