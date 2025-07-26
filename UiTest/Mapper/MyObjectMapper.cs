using System;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace UiTest.Mapper
{
    public static class MyObjectMapper
    {
        public static bool Update<T1, T2>(T1 objSrc, T2 objTarget)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter("AutoMapper", LogLevel.Warning);
            });
            if (objSrc == null || objTarget == null) return false;
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>()
                   .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            }, loggerFactory);
            var mapper = config.CreateMapper();
            mapper.Map(objSrc, objTarget);
            return true;
        }
        public static bool Copy<T1, T2>(T1 objSrc, T2 objTarget)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter("AutoMapper", LogLevel.Warning);
            });
            if (objSrc == null || objTarget == null) return false;
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>()
                   .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => true));
            }, loggerFactory);
            var mapper = config.CreateMapper();
            mapper.Map(objSrc, objTarget);
            return true;
        }
    }
}
