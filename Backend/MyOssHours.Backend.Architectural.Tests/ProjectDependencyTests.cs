using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace MyOssHours.Backend.Architectural.Tests
{
    public class ProjectDependencyTests
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        private static readonly string DomainNamespace = typeof(Domain.AssemblyReference).Namespace;
        private static readonly string ApplicationNamespace = typeof(Application.AssemblyReference).Namespace;
        private static readonly string InfrastructureNamespace = typeof(Infrastructure.AssemblyReference).Namespace;
        private static readonly string PresentationNamespace = typeof(Presentation.AssemblyReference).Namespace;
        private static readonly string WebNamespace = typeof(REST.AssemblyReference).Namespace;
#pragma warning restore CS8601 // Possible null reference assignment.

        [Fact]
        public void Domain_Should_Not_HaveDependencyToOtherProjects()
        {
            // Arrange
            var assembly = typeof(Domain.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_HaveDependencyToOtherProjects()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Infrastructure_Should_Not_HaveDependencyToOtherProjects()
        {
            // Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                PresentationNamespace,
                WebNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Presentation_Should_Not_HaveDependencyToOtherProjects()
        {
            // Arrange
            var assembly = typeof(Presentation.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                InfrastructureNamespace,
                WebNamespace
            };

            // Act
            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

    }
}
