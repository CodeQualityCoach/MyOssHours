﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace MyOssHours.Backend.Specs.Tests
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class ProjectFeature : object, Xunit.IClassFixture<ProjectFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Project.feature"
#line hidden
        
        public ProjectFeature(ProjectFeature.FixtureData fixtureData, MyOssHours_Backend_Specs_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "", "Project", "As a user\r\nI want to create, read, and delete a project\r\nSo this I can manage my " +
                    "projects", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 7
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "description",
                        "permissions"});
            table1.AddRow(new string[] {
                        "Demo_01",
                        "The is project demo 01",
                        "alice=owner, bob=none"});
            table1.AddRow(new string[] {
                        "Demo_02",
                        "This is project demo 02",
                        "alice=owner, bob=reader"});
            table1.AddRow(new string[] {
                        "Demo_03",
                        "The is project demo 03",
                        "bob=owner, alice=none"});
            table1.AddRow(new string[] {
                        "Demo_04",
                        "This is project demo 04",
                        "bob=owner, alice=reader"});
            table1.AddRow(new string[] {
                        "Demo_05",
                        "The project will be deleted",
                        "bob=owner, alice=reader"});
#line 8
 testRunner.Given("the following projects exist:", ((string)(null)), table1, "Given ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Create Project")]
        [Xunit.TraitAttribute("FeatureTitle", "Project")]
        [Xunit.TraitAttribute("Description", "Create Project")]
        [Xunit.TraitAttribute("Category", "project")]
        [Xunit.InlineDataAttribute("Demo_01", "alice", new string[0])]
        [Xunit.InlineDataAttribute("Demo_02", "bob", new string[0])]
        public void CreateProject(string name, string id, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "project"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("name", name);
            argumentsOfScenario.Add("id", id);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create Project", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 17
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 18
 testRunner.Given(string.Format("The user with id \'{0}\' is logged in", id), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 19
 testRunner.When(string.Format("The user creates a new project with the name \'{0}\'", name), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 20
 testRunner.Then(string.Format("The project with the name \'{0}\' is created", name), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Read Project")]
        [Xunit.TraitAttribute("FeatureTitle", "Project")]
        [Xunit.TraitAttribute("Description", "Read Project")]
        [Xunit.TraitAttribute("Category", "project")]
        public void ReadProject()
        {
            string[] tagsOfScenario = new string[] {
                    "project"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Read Project", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 28
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 29
 testRunner.Given("the user Alice is logged in", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "name",
                            "description"});
                table2.AddRow(new string[] {
                            "Demo_01",
                            "The is project demo 01"});
                table2.AddRow(new string[] {
                            "Demo_02",
                            "This is project demo 02"});
#line 30
 testRunner.Given("the following projects exist for user alice:", ((string)(null)), table2, "Given ");
#line hidden
#line 34
 testRunner.When("the user alice reads the existing projects", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 35
 testRunner.Then("the result contains a project with the name \'Demo_01\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 36
 testRunner.Then("the result contains a project with the name \'Demo_02\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Delete Project")]
        [Xunit.TraitAttribute("FeatureTitle", "Project")]
        [Xunit.TraitAttribute("Description", "Delete Project")]
        [Xunit.TraitAttribute("Category", "project")]
        public void DeleteProject()
        {
            string[] tagsOfScenario = new string[] {
                    "project"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Delete Project", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 39
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 40
 testRunner.Given("the user Alice is logged in", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "name",
                            "description"});
                table3.AddRow(new string[] {
                            "Demo_01",
                            "The is project demo 01"});
                table3.AddRow(new string[] {
                            "Demo_02",
                            "This is project demo 02"});
#line 41
 testRunner.Given("the following projects exist for user alice:", ((string)(null)), table3, "Given ");
#line hidden
#line 45
 testRunner.When("the user alice deletes the project with the name \'Demo_02\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 46
 testRunner.Then("the result contains a project with the name \'Demo_01\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 47
 testRunner.Then("the result does not contain a project with the name \'Demo_02\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Cannot Delete Project without owner permission")]
        [Xunit.TraitAttribute("FeatureTitle", "Project")]
        [Xunit.TraitAttribute("Description", "Cannot Delete Project without owner permission")]
        [Xunit.TraitAttribute("Category", "project")]
        public void CannotDeleteProjectWithoutOwnerPermission()
        {
            string[] tagsOfScenario = new string[] {
                    "project"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Cannot Delete Project without owner permission", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 50
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 51
 testRunner.Given("the user Alice is logged in", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "name",
                            "description"});
                table4.AddRow(new string[] {
                            "Demo_01",
                            "The is project demo 01"});
#line 52
 testRunner.Given("the following projects exist for user alice:", ((string)(null)), table4, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "name",
                            "description"});
                table5.AddRow(new string[] {
                            "Demo_02",
                            "This is project demo 02"});
#line 55
 testRunner.Given("the following projects exist for user bob:", ((string)(null)), table5, "Given ");
#line hidden
#line 58
 testRunner.When("the user alice deletes the project with the name \'Demo_02\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 59
 testRunner.Then("a 403 is returned", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Read projects I have permissions for")]
        [Xunit.TraitAttribute("FeatureTitle", "Project")]
        [Xunit.TraitAttribute("Description", "Read projects I have permissions for")]
        [Xunit.TraitAttribute("Category", "project")]
        public void ReadProjectsIHavePermissionsFor()
        {
            string[] tagsOfScenario = new string[] {
                    "project"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Read projects I have permissions for", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 62
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 63
 testRunner.Given("the user Alice is logged in", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "name",
                            "description"});
                table6.AddRow(new string[] {
                            "Demo_01",
                            "The is project demo 01"});
                table6.AddRow(new string[] {
                            "Demo_02",
                            "This is project demo 02"});
#line 64
 testRunner.Given("the following projects exist for user alice:", ((string)(null)), table6, "Given ");
#line hidden
#line 68
 testRunner.Given("the user Bob is logged in", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "name",
                            "description"});
                table7.AddRow(new string[] {
                            "Demo_03",
                            "The is project demo 03"});
                table7.AddRow(new string[] {
                            "Demo_04",
                            "This is project demo 04"});
#line 69
 testRunner.Given("the following projects exist for user bob:", ((string)(null)), table7, "Given ");
#line hidden
#line 73
 testRunner.When("the user alice reads the existing projects", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 74
 testRunner.Then("the result contains a project with the name \'Demo_01\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 75
 testRunner.Then("the result contains a project with the name \'Demo_02\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 76
 testRunner.Then("the result does not contain a project with the name \'Demo_03\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 77
 testRunner.Then("the result does not contain a project with the name \'Demo_04\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                ProjectFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                ProjectFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
