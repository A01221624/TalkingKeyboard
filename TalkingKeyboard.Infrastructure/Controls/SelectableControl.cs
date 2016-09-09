﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectableControl.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.Controls
{
    using System;
    using System.Media;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    /// <summary>
    ///     Base for selectable controls
    /// </summary>
    public abstract class SelectableControl : ContentControl, ICommandSource, IComparable
    {
        /// <summary>
        ///     The animation property
        /// </summary>
        public static readonly DependencyProperty AnimationProperty = DependencyProperty.Register(
            "Animation",
            typeof(Storyboard),
            typeof(SelectableControl),
            new PropertyMetadata(default(Storyboard)));

        /// <summary>
        ///     The command parameter property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(SelectableControl),
                new PropertyMetadata(default(object)));

        /// <summary>
        ///     The command property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(SelectableControl),
            new PropertyMetadata(default(ICommand)));

        /// <summary>
        ///     The command target property
        /// </summary>
        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register(
            "CommandTarget",
            typeof(IInputElement),
            typeof(SelectableControl),
            new PropertyMetadata(default(IInputElement)));

        /// <summary>
        ///     Gets or sets the animation.
        /// </summary>
        public Storyboard Animation
        {
            get
            {
                return (Storyboard)this.GetValue(AnimationProperty);
            }

            set
            {
                this.SetValue(AnimationProperty, value);
            }
        }

        /// <summary>
        ///     Gets or sets the command.
        /// </summary>
        public ICommand Command
        {
            get
            {
                return (ICommand)this.GetValue(CommandProperty);
            }

            set
            {
                this.SetValue(CommandProperty, value);
            }
        }

        /// <summary>
        ///     Gets or sets the command parameter.
        /// </summary>
        public object CommandParameter
        {
            get
            {
                return this.GetValue(CommandParameterProperty);
            }

            set
            {
                this.SetValue(CommandParameterProperty, value);
            }
        }

        /// <summary>
        ///     Gets or sets the command target.
        /// </summary>
        public IInputElement CommandTarget
        {
            get
            {
                return (IInputElement)this.GetValue(CommandTargetProperty);
            }

            set
            {
                this.SetValue(CommandTargetProperty, value);
            }
        }

        /// <summary>
        ///     Implementation for IComparable.
        /// </summary>
        /// <param name="obj">
        ///     Object to compare against.
        /// </param>
        /// <returns>
        ///     0 if equal, -1 if the parameter is not of same type, 1 otherwise.
        /// </returns>
        public int CompareTo(object obj)
        {
            var other = obj as SelectableControl;
            if (other == null)
            {
                return -1;
            }

            if (other.Uid == this.Uid)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        ///     Pauses the animation.
        /// </summary>
        public void PauseAnimation()
        {
            this.Animation.Pause();
        }

        /// <summary>
        ///     Plays the animation.
        /// </summary>
        public void PlayAnimation()
        {
            this.Animation.Begin();
        }

        /// <summary>
        ///     Resumes the animation.
        /// </summary>
        public void ResumeAnimation()
        {
            this.Animation.Resume();
        }

        /// <summary>
        ///     Executes the command assigned to the control.
        /// </summary>
        public void Select()
        {
            if (this.Command == null)
            {
                return;
            }

            var canExecute = this.Command.CanExecute(this.CommandParameter);
            if (!canExecute)
            {
                return;
            }

            this.Command?.Execute(this.CommandParameter);
            SystemSounds.Hand.Play();
        }

        /// <summary>
        ///     Stops the animation.
        /// </summary>
        public void StopAnimation()
        {
            this.Animation.Stop();
        }
    }
}