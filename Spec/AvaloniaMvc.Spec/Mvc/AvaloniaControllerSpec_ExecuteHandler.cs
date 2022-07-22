// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Mvc;

[Context("Executes handlers")]
class AvaloniaControllerSpec_ExecuteHandler : FixtureSteppable
{
    bool ChangedEventHandled { get; set; }

    [Example("Retrieves event handlers and executes them when an element is not attached")]
    void Ex01()
    {
        When("the Changed event is raised using the EventHandlerBase", () =>
            AvaloniaController.EventHandlersOf(new TestAvaloniaControllers.TestAvaloniaController { ChangedAssertionHandler = () => ChangedEventHandled = true })
                .GetBy("Element")
                .Raise("Changed")
        );
        Then("the Changed event should be handled", () => ChangedEventHandled);
    }

    [Example("Retrieves event handlers and executes them asynchronously when an element is not attached")]
    void Ex02()
    {
        When("the Changed event is raised using the EventHandlerBase", async () =>
            await AvaloniaController.EventHandlersOf(new TestAvaloniaControllers.TestAvaloniaController { ChangedAssertionHandler = () => ChangedEventHandled = true })
                .GetBy("Element")
                .RaiseAsync("Changed")
        );
        Then("the Changed event should be handled", () => ChangedEventHandled);
    }
}