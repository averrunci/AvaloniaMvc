// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;
using Carna;

namespace Charites.Windows.Mvc;

[Context("AvaloniaControllerExtension")]
class AvaloniaControllerSpec_AvaloniaControllerExtension : FixtureSteppable, IDisposable
{
    class TestExtension : IAvaloniaControllerExtension
    {
        public static object TestExtensionContainer { get; } = new();
        public bool Attached { get; private set; }
        public bool Detached { get; private set; }
        void IControllerExtension<StyledElement>.Attach(object controller, StyledElement element) => Attached = true;
        void IControllerExtension<StyledElement>.Detach(object controller, StyledElement element) => Detached = true;
        object IControllerExtension<StyledElement>.Retrieve(object controller) => TestExtensionContainer;
    }

    TestAvaloniaControllers.TestAvaloniaController Controller { get; } = new();
    TestExtension Extension { get; } = new();
    TestElement Element { get; } = new() { DataContext = new TestDataContexts.TestDataContext() };

    public void Dispose()
    {
        AvaloniaController.RemoveExtension(Extension);
    }

    [Example("Attaches an extension when the element is attached to logical tree and detaches it when the element is detached from logical tree")]
    void Ex01()
    {
        When("an extension is added to the AvaloniaController", () => AvaloniaController.AddExtension(Extension));
        When("the AvaloniaController is enabled for the element", () => AvaloniaController.SetIsEnabled(Element, true));

        When("the element is attached to the logical tree", () => Element.AttachToLogicalTree());
        Then("the extension should be attached", () => Extension.Attached);

        When("the element is detached from the logical tree", () => Element.DetachFromLogicalTree());
        Then("the extension should be detached", () => Extension.Detached);
    }

    [Example("Retrieves a container of an extension")]
    void Ex02()
    {
        When("an extension is added to the AvaloniaController", () => AvaloniaController.AddExtension(Extension));
        Then("the container of the extension should be retrieved", () => AvaloniaController.Retrieve<TestExtension, object>(Controller) == TestExtension.TestExtensionContainer);
    }
}