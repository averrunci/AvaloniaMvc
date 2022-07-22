// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
using Carna;

namespace Charites.Windows.Mvc;

[Context("Attaching and Detaching a controller")]
class AvaloniaControllerSpec_AttachingAndDetachingController : FixtureSteppable
{
    TestElement Element { get; set; } = default!;
    TestAvaloniaControllers.TestAvaloniaController Controller { get; set; } = default!;

    bool AttachedToLogicalTreeEventHandled { get; set; }
    bool ChangedEventHandled { get; set; }
    TestAvaloniaControllers.TestAvaloniaControllerBase[] Controllers { get; set; } = default!;
    bool[] AttachedToLogicalTreeEventsHandled { get; set; } = default!;
    bool[] ChangedEventsHandled { get; set; } = default!;

    class AttachingControllerSampleDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "When the key is the name of the data context type",
                DataContext = new TestDataContexts.AttachingTestDataContext(),
                ExpectedControllerTypes = new[] { typeof(TestAvaloniaControllers.TestDataContextController) }
            };
            yield return new
            {
                Description = "When the key is the name of the data context base type",
                DataContext = new TestDataContexts.DerivedBaseAttachingTestDataContext(),
                ExpectedControllerTypes = new[] { typeof(TestAvaloniaControllers.BaseTestDataContextController) }
            };
            yield return new
            {
                Description = "When the key is the full name of the data context type",
                DataContext = new TestDataContexts.AttachingTestDataContextFullName(),
                ExpectedControllerTypes = new[] { typeof(TestAvaloniaControllers.TestDataContextFullNameController) }
            };
            yield return new
            {
                Description = "When the key is the full name of the data context base type",
                DataContext = new TestDataContexts.DerivedBaseAttachingTestDataContextFullName(),
                ExpectedControllerTypes = new[] { typeof(TestAvaloniaControllers.BaseTestDataContextFullNameController) }
            };
            yield return new
            {
                Description = "When the key is the name of the data context type that is generic",
                DataContext = new TestDataContexts.GenericAttachingTestDataContext<string>(),
                ExpectedControllerTypes = new[] { typeof(TestAvaloniaControllers.GenericTestDataContextController) }
            };
            yield return new
            {
                Description = "When the key is the full name of the data context type that is generic",
                DataContext = new TestDataContexts.GenericAttachingTestDataContextFullName<string>(),
                ExpectedControllerTypes = new[] { typeof(TestAvaloniaControllers.GenericTestDataContextFullNameController), typeof(TestAvaloniaControllers.GenericTestDataContextFullNameWithoutParametersController) }
            };
            yield return new
            {
                Description = "When the key is the name of the interface implemented by the data context",
                DataContext = new TestDataContexts.InterfaceImplementedAttachingTestDataContext(),
                ExpectedControllerTypes = new[] { typeof(TestAvaloniaControllers.InterfaceImplementedTestDataContextController) }
            };
            yield return new
            {
                Description = "When the key is the full name of the interface implemented by the data context",
                DataContext = new TestDataContexts.InterfaceImplementedAttachingTestDataContextFullName(),
                ExpectedControllerTypes = new[] { typeof(TestAvaloniaControllers.InterfaceImplementedTestDataContextFullNameController) }
            };
        }
    }

    [Example("Attaches a controller when the IsEnabled property of the AvaloniaController is set to true")]
    [Sample(Source = typeof(AttachingControllerSampleDataSource))]
    void Ex01(object dataContext, IEnumerable<Type> expectedControllerTypes)
    {
        Given("an element that contains the data context", () => Element = new TestElement { DataContext = dataContext });
        When("the AvaloniaController is enabled for the element", () => AvaloniaController.SetIsEnabled(Element, true));
        Then("the controller should be attached to the element", () =>
            AvaloniaController.GetControllers(Element).Select(controller => controller.GetType()).SequenceEqual(expectedControllerTypes) &&
            AvaloniaController.GetControllers(Element).OfType<TestAvaloniaControllers.TestController>().All(controller => controller.DataContext == Element.DataContext)
        );
    }

    [Example("Attaches a controller when the IsEnabled property of the AvaloniaController is set to true before the data context of the element is set")]
    [Sample(Source = typeof(AttachingControllerSampleDataSource))]
    void Ex02(object dataContext, IEnumerable<Type> expectedControllerTypes)
    {
        Given("an element that does not contain the data context", () => Element = new TestElement());
        When("the AvaloniaController is enabled for the element", () => AvaloniaController.SetIsEnabled(Element, true));
        When("a data context of the element is set", () => Element.DataContext = dataContext);
        Then("the controller should be attached to the element", () =>
            AvaloniaController.GetControllers(Element).Select(controller => controller.GetType()).SequenceEqual(expectedControllerTypes) &&
            AvaloniaController.GetControllers(Element).OfType<TestAvaloniaControllers.TestController>().All(controller => controller.DataContext == Element.DataContext)
        );
    }

    [Example("Attaches a controller when the Key property of the AvaloniaController is set")]
    void Ex03()
    {
        Given("an element that contains the data context", () => Element = new TestElement { DataContext = new TestDataContexts.KeyAttachingTestDataContext() });
        When("the key of the element is set using the AvaloniaController", () => AvaloniaController.SetKey(Element, "TestElement"));
        Then("the AvaloniaController should be enabled for the element", () => AvaloniaController.GetIsEnabled(Element));
        Then("the controller should be attached to the element", () =>
            AvaloniaController.GetControllers(Element).Select(controller => controller.GetType()).SequenceEqual(new[] { typeof(TestAvaloniaControllers.KeyTestDataContextController) }) &&
            AvaloniaController.GetControllers(Element).OfType<TestAvaloniaControllers.TestController>().All(controller => controller.DataContext == Element.DataContext)
        );
    }

    [Example("Sets the changed data context when the AvaloniaController is enabled and the data context of an element is changed")]
    void Ex04()
    {
        Given("an element that contains the data context", () => Element = new TestElement { DataContext = new TestDataContexts.AttachingTestDataContext() });
        When("the AvaloniaController is enabled for the element", () => AvaloniaController.SetIsEnabled(Element, true));
        Then("the data context of the controller should be set", () => Element.GetController<TestAvaloniaControllers.TestController>().DataContext == Element.DataContext);
        When("another data context is set for the element", () => Element.DataContext = new object());
        Then("the data context of the controller should be set", () => Element.GetController<TestAvaloniaControllers.TestController>().DataContext == Element.DataContext);
    }

    [Example("Removes event handlers and sets null to elements and a data context when the DetachedFromLogicalTree event of the root element is raised")]
    void Ex05()
    {
        Given("an element that contains the data context", () => Element = new TestElement { Name = "Element", DataContext = new TestDataContexts.TestDataContext() });

        When("the AvaloniaController is enabled for the element", () =>
        {
            AvaloniaController.SetIsEnabled(Element, true);
            Controller = Element.GetController<TestAvaloniaControllers.TestAvaloniaController>();
        });
        Then("the data context of the controller should be set", () => Controller.DataContext == Element.DataContext);

        When("the element is attache to the logical tree", () =>
        {
            Controller.AttachedToLogicalTreeAssertionHandler = () => AttachedToLogicalTreeEventHandled = true;
            Element.AttachToLogicalTree();
        });
        Then("the element of the controller should be set", () => Controller.Element == Element);
        Then("the AttachedToLogicalTree event should be handled", () => AttachedToLogicalTreeEventHandled);

        When("the Changed event is raised", () =>
        {
            Controller.ChangedAssertionHandler = () => ChangedEventHandled = true;
            Element.RaiseChanged();
        });
        Then("the Changed event should be handled", () => ChangedEventHandled);

        When("the element is detached from the logical tree", () => Element.DetachFromLogicalTree());
        Then("the data context of the controller should be null", () => Controller.DataContext == null);
        Then("the element of the controller should be null", () => Controller.Element == null);

        When("the element is attached to the logical tree", () =>
        {
            AttachedToLogicalTreeEventHandled = false;
            Element.AttachToLogicalTree();
        });
        Then("the AttachedToLogicalTree event should not be handled", () => !AttachedToLogicalTreeEventHandled);

        When("the Changed event is raised", () =>
        {
            ChangedEventHandled = false;
            Element.RaiseChanged();
        });
        Then("the Changed event should not be handled", () => !ChangedEventHandled);
    }

    [Example("Removes event handlers and sets null to elements and a data context when the AvaloniaController is disabled")]
    void Ex06()
    {
        Given("an element that contains the data context", () => Element = new TestElement { Name = "Element", DataContext = new TestDataContexts.TestDataContext() });

        When("the AvaloniaController is enabled for the element", () =>
        {
            AvaloniaController.SetIsEnabled(Element, true);
            Controller = Element.GetController<TestAvaloniaControllers.TestAvaloniaController>();
        });
        Then("the data context of the controller should be set", () => Controller.DataContext == Element.DataContext);

        When("the element is attached to the logical tree", () =>
        {
            Controller.AttachedToLogicalTreeAssertionHandler = () => AttachedToLogicalTreeEventHandled = true;
            Element.AttachToLogicalTree();
        });
        Then("the element of the controller should be set", () => Controller.Element == Element);
        Then("the AttachedToLogicalTree event should be handled", () => AttachedToLogicalTreeEventHandled);

        When("the Changed event is raised", () =>
        {
            Controller.ChangedAssertionHandler = () => ChangedEventHandled = true;
            Element.RaiseChanged();
        });
        Then("the Changed event should be handled", () => ChangedEventHandled);

        When("the element is detached from the logical tree", () => Element.DetachFromLogicalTree());
        When("the AvaloniaController is disabled for the element", () => AvaloniaController.SetIsEnabled(Element, false));
        Then("the controller should be detached", () => !AvaloniaController.GetControllers(Element).Any());
        Then("the data context of the controller should be null", () => Controller.DataContext == null);
        Then("the element of the controller should be null", () => Controller.Element == null);

        When("the element is attached to the logical tree", () =>
        {
            AttachedToLogicalTreeEventHandled = false;
            Element.AttachToLogicalTree();
        });
        Then("the AttachedToLogicalTree event should not be handled", () => !AttachedToLogicalTreeEventHandled);
        Then("the controller should not be attached", () => !AvaloniaController.GetControllers(Element).Any());

        When("the Changed event is raised", () =>
        {
            ChangedEventHandled = false;
            Element.RaiseChanged();
        });
        Then("the Changed event should not be handled", () => !ChangedEventHandled);
    }

    [Example("Attaches multi controllers")]
    void Ex07()
    {
        Given("an element that contains the data context", () => Element = new TestElement { Name = "Element", DataContext = new TestDataContexts.MultiTestDataContext() });

        When("the AvaloniaController is enabled for the element", () =>
        {
            AvaloniaController.SetIsEnabled(Element, true);
            Controllers = new TestAvaloniaControllers.TestAvaloniaControllerBase[]
            {
                Element.GetController<TestAvaloniaControllers.MultiTestAvaloniaControllerA>(),
                Element.GetController<TestAvaloniaControllers.MultiTestAvaloniaControllerB>(),
                Element.GetController<TestAvaloniaControllers.MultiTestAvaloniaControllerC>()
            };
            AttachedToLogicalTreeEventsHandled = new[] { false, false, false };
            ChangedEventsHandled = new[] { false, false, false };
        });
        Then("the data context of the controller should be set", () => Controllers.All(controller => controller.DataContext == Element.DataContext));

        When("the element is attached to the logical tree", () =>
        {
            for (var index = 0; index < Controllers.Length; index++)
            {
                var eventHandledIndex = index;
                Controllers[index].AttachedToLogicalTreeAssertionHandler = () => AttachedToLogicalTreeEventsHandled[eventHandledIndex] = true;
            }
            Element.AttachToLogicalTree();
        });
        Then("the element of the controller should be set", () => Controllers.All(controller => controller.Element == Element));
        Then("the AttachedToLogicalTree event should be handled", () => AttachedToLogicalTreeEventsHandled.All(handled => handled));

        When("the Changed event is raised", () =>
        {
            for (var index = 0; index < Controllers.Length; index++)
            {
                var eventHandledIndex = index;
                Controllers[index].ChangedAssertionHandler = () => ChangedEventsHandled[eventHandledIndex] = true;
            }
            Element.RaiseChanged();
        });
        Then("the Changed event should be handled", () => ChangedEventsHandled.All(handled => handled));

        When("the element is detached from the logical tree", () => Element.DetachFromLogicalTree());
        Then("the data context of the controller should be null", () => Controllers.All(controller => controller.DataContext == null));
        Then("the element of the controller should be null", () => Controllers.All(controller => controller.Element == null));

        When("the element is attached to the logical tree", () =>
        {
            for (var index = 0; index < AttachedToLogicalTreeEventsHandled.Length; index++)
            {
                AttachedToLogicalTreeEventsHandled[index] = false;
            }
            Element.AttachToLogicalTree();
        });
        Then("the AttachedToLogicalTree event should not be handled", () => AttachedToLogicalTreeEventsHandled.All(handled => !handled));

        When("the Changed event is raised", () =>
        {
            for (var index = 0; index < ChangedEventsHandled.Length; index++)
            {
                ChangedEventsHandled[index] = false;
            }
            Element.RaiseChanged();
        });
        Then("the Changed event should not be handled", () => ChangedEventsHandled.All(handled => !handled));
    }
}