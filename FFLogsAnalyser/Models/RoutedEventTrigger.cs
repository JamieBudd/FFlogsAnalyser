﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace FFLogsAnalyser.Models
{
    public class RoutedEventTrigger : EventTriggerBase<DependencyObject>
    {
        RoutedEvent _routedEvent;
        public RoutedEvent RoutedEvent
        {
            get { return _routedEvent; }
            set { _routedEvent = value; }
        }
        public string EventName { get; set; }

        public RoutedEventTrigger() { }
        protected override void OnAttached()
        {
            switch (EventName)
            {
                case "DragQuery":
                    RoutedEvent = UIElement.DragEnterEvent;
                    break;
                case "DropQuery":
                    RoutedEvent = UIElement.DropEvent;
                    break;
                default:
                    break;
            }

            Behavior behavior = base.AssociatedObject as Behavior;
            FrameworkElement associatedElement = base.AssociatedObject as FrameworkElement;
            if (behavior != null)
            {
                associatedElement = ((IAttachedObject)behavior).AssociatedObject as FrameworkElement;
            }
            if (associatedElement == null)
            {
                throw new ArgumentException("Routed Event trigger can only be associated to framework elements");
            }
            if (RoutedEvent != null)
            { associatedElement.AddHandler(RoutedEvent, new RoutedEventHandler(this.OnRoutedEvent)); }
        }
        void OnRoutedEvent(object sender, RoutedEventArgs args)
        {
            base.OnEvent(args);
        }
        protected override string GetEventName() { return RoutedEvent.Name; }
    }
}
