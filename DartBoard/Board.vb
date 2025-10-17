'Payden Hoskins
'10/17/25
'RCET3371
'Dart Board
'https://github.com/PaydenHoskins/DartBoard.git


Option Explicit On
Option Strict On

Imports System.Linq.Expressions

Public Class Board
    Public filePath As String = "DartScoreAndRound.txt"
    Public roundNumber As Integer
    Public reviewY As Integer
    Public reviewX As Integer

    'Event Handlers
    Private Sub ThrowButton_Click(sender As Object, e As EventArgs) Handles ThrowButton.Click

        Dim DartCount As Integer
        Do
            PictureBox1.Refresh()
            roundNumber += 1
            Dim X As Integer
            Dim Y As Integer
            WriteLine(1, "Round" & CStr(roundNumber))
            WriteLine(1)
            'Thow three darts per round
            For i = 1 To 3
                X = RandomNumber(PictureBox1.Width)
                Y = RandomNumber(PictureBox1.Height)
                DartCount += 1
                WriteLine(1, $"Dart {DartCount}")
                WriteLine(1, "X Coordinate: " & CStr(X) & " Y Coordinate: " & CStr(Y))
                WriteLine(1)
                DrawDart(X, Y)
                ComboBox1.Items.Add("Round" & CStr(roundNumber) & ":" & " " & "x:" & X & ":" & "y:" & Y)
            Next
            DartCount = 0
            'Prompt to throw again
        Loop Until MsgBox("3 Darts Thrown, would you like to throw again?", MsgBoxStyle.YesNo) = MsgBoxResult.No
        'Prompt to clear board
        If MsgBox("would you like to clear the board?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            PictureBox1.Refresh()
            Defaults()
        End If
    End Sub

    Private Sub Board_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Defaults()
        FileOpen(1, filePath, OpenMode.Append)
    End Sub

    'Functions and Subroutines
    Function RandomNumber(max%) As Integer
        Randomize()
        Return CInt(System.Math.Floor(CDbl(Rnd() * max))) + 1
    End Function

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        WriteLine(1, "----Game Ended----")
        FileClose(1)
        Me.Close()
    End Sub

    Sub DrawDart(x%, y%)
        Dim g As Graphics = PictureBox1.CreateGraphics
        Dim pen As New Pen(Color.Red)
        Dim dartWidth% = 30

        g.DrawEllipse(pen, x - (dartWidth \ 2), y - (dartWidth \ 2), dartWidth, dartWidth)
        g.DrawLine(pen, x - 3, y, x + 3, y)
        g.DrawLine(pen, x, y - 3, x, y + 3)

        pen.Dispose()
        g.Dispose()
    End Sub

    Sub DrawDart2(x%, y%)
        Dim g As Graphics = PictureBox2.CreateGraphics
        Dim pen As New Pen(Color.Red)
        Dim dartWidth% = 30

        g.DrawEllipse(pen, x - (dartWidth \ 2), y - (dartWidth \ 2), dartWidth, dartWidth)
        g.DrawLine(pen, x - 3, y, x + 3, y)
        g.DrawLine(pen, x, y - 3, x, y + 3)

        pen.Dispose()
        g.Dispose()
    End Sub

    Sub Defaults()
        PictureBox1.Refresh()
        PictureBox1.BackColor = Color.DarkGray
        PictureBox2.Refresh()
        PictureBox2.BackColor = Color.DarkGray
        ListBox1.Items.Clear()
    End Sub

    Private Sub ReviewButton_Click(sender As Object, e As EventArgs) Handles ReviewButton.Click
        Dim currentRecord As String = ""
        Defaults()
        TabControl1.SelectedTab = TabPage2
        'Read and display the log file to listbox
        ListBox1.Items.Clear()
        FileClose(1)
        FileOpen(2, filePath, OpenMode.Input)
        Do Until EOF(2)
            Input(2, currentRecord)
            Dim data As String = currentRecord
            ListBox1.Items.Add(data)
        Loop
    End Sub

    Private Sub GameButton_Click(sender As Object, e As EventArgs) Handles GameButton.Click
        Defaults()
        FileClose(2)
        TabControl1.SelectedTab = TabPage1
        ListBox1.Items.Clear()
        Defaults()
        FileOpen(1, filePath, OpenMode.Append)
    End Sub

    Private Sub DartDisplayButton_Click(sender As Object, e As EventArgs) Handles DartDisplayButton.Click
        Dim displayX As Integer
        Dim displayY As Integer
        Dim coordnates As String
        Dim coords() As String
        If ComboBox1.SelectedItem Is Nothing Then
            MsgBox("Select a dart to display")
        ElseIf ComboBox1.SelectedItem IsNot Nothing Then
            coordnates = ComboBox1.SelectedItem.ToString()
            coords = Split(coordnates, ":")
            displayX = CInt(coords(2))
            displayY = CInt(coords(4))
            DrawDart2(displayX, displayY)
        End If
    End Sub
    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        Defaults()
    End Sub
End Class
