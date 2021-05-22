using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Sneha_BL;

namespace SnehaProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ISchoolRepositary, SchoolRepositary>();
            container.RegisterType<IGradeRepositary, GradeRepositary>();
            container.RegisterType<ISubjectRepositary, SubjectRepositary>();
            container.RegisterType<IScoreRepository, ScoreRepository>();
            container.RegisterType<IChartRepositary, ChartRepositary>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
           
        }
    }
}