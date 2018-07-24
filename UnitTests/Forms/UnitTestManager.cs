﻿#region Namespace

using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

using UnitTests.Tests;

using VisualPlus.Events;
using VisualPlus.Structure;
using VisualPlus.Toolkit.Dialogs;

#endregion

namespace UnitTests.Forms
{
    /// <summary>The test manager.</summary>
    public partial class UnitTestManager : VisualForm
    {
        #region Variables

        private UnitTests _unitTest;

        #endregion

        #region Constructors

        public UnitTestManager()
        {
            InitializeComponent();
            HelpButtonClicked += VisualControlBox_HelpClick;
        }

        #endregion

        #region Enumerators

        /// <summary>The different kind of unit tests.</summary>
        private enum UnitTests
        {
            /// <summary>The VisualForm.</summary>
            VisualForm = 0,

            /// <summary>The VisualControlBox.</summary>
            VisualControlBox = 1,

            /// <summary>The list view.</summary>
            VisualListView = 2,

            /// <summary>The visual exception dialog.</summary>
            VisualExceptionDialog = 3,

            /// <summary>The visual input box.</summary>
            VisualInputBox = 4,

            /// <summary>The visual message box.</summary>
            VisualMessageBox = 5
        }

        #endregion

        #region Methods

        private void BtnRunTest_Click(object sender, EventArgs e)
        {
            VisualForm _formToOpen;

            switch (_unitTest)
            {
                case UnitTests.VisualForm:
                    {
                        _formToOpen = new VisualForm($@"{nameof(VisualForm)} Test");
                        _formToOpen.ShowDialog();
                        break;
                    }

                case UnitTests.VisualControlBox:
                    {
                        _formToOpen = new VisualControlBoxTest();
                        _formToOpen.ShowDialog();
                        break;
                    }

                case UnitTests.VisualListView:
                    {
                        _formToOpen = new VisualListViewTest();
                        _formToOpen.ShowDialog();
                        break;
                    }

                case UnitTests.VisualMessageBox:
                    {
                        _formToOpen = new VisualMessageBoxTest();
                        _formToOpen.ShowDialog();
                        break;
                    }

                case UnitTests.VisualInputBox:
                    {
                        VisualInputBox inputBox = new VisualInputBox($@"{nameof(VisualInputBox)} Test");

                        if (inputBox.ShowDialog() == DialogResult.OK)
                        {
                            ConsoleEx.WriteDebug(inputBox.InputResult);
                        }

                        break;
                    }

                case UnitTests.VisualExceptionDialog:
                    {
                        VisualExceptionDialog.Show(new Exception("Your custom exception message."));
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
            }
        }

        /// <summary>Generates the test statistics.</summary>
        /// <returns>The <see cref="string" />.</returns>
        private string GenerateTestStatistics()
        {
            StringBuilder stats = new StringBuilder();
            stats.AppendLine($@"Selected: {visualListBoxTests.SelectedIndex + 1}");
            stats.AppendLine($@"Total Tests: {visualListBoxTests.Items.Count}");
            return stats.ToString();
        }

        private void ListBoxTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            _unitTest = (UnitTests)visualListBoxTests.SelectedIndex;
            visualLabelTestsStats.Text = GenerateTestStatistics();
        }

        private void TestManager_Load(object sender, EventArgs e)
        {
            _unitTest = UnitTests.VisualListView;

            Array _tests = typeof(UnitTests).GetEnumValues();
            visualLabelTestsStats.Text = GenerateTestStatistics();

            foreach (object test in _tests)
            {
                visualListBoxTests.Items.Add(test);
            }

            visualListBoxTests.SelectedIndex = 0;
        }

        private void VisualControlBox_HelpClick(ControlBoxEventArgs e)
        {
            Process.Start("https://darkbyte7.github.io/VisualPlus/");
        }

        #endregion
    }
}