
Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class Form1


    ' MongoDB Connection String
    Private connectionString As String = "mongodb+srv://jasaga:igywFraEIYcgiMZG@cluster0.sd0fg.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"

    Private client As MongoClient
    Private database As IMongoDatabase
    Private collection As IMongoCollection(Of BsonDocument)
    Private documents As List(Of BsonDocument)
    Private currentIndex As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ' Initialize MongoDB client and access collection
            client = New MongoClient(connectionString)
            database = client.GetDatabase("dbcustomer")
            collection = database.GetCollection(Of BsonDocument)("users")

            ' Load all documents into a list
            documents = collection.Find(New BsonDocument()).ToList()

            If documents.Count > 0 Then
                DisplayRecord(currentIndex)
            Else
                MessageBox.Show("No records found.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    Private Sub DisplayRecord(index As Integer)

        If index >= 0 And index < documents.Count Then

            Dim doc = documents(index)
            txtTitle.Text = doc.GetValue("title", "").AsString
            txtForename.Text = doc.GetValue("forename", "").AsString
            txtSurname.Text = doc.GetValue("surname", "").AsString
            lblRecordIndex.Text = $"Record {index + 1} of {documents.Count}"

        End If

    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        If currentIndex < documents.Count - 1 Then
            currentIndex += 1
            DisplayRecord(currentIndex)
        Else
            MessageBox.Show("You are at the last record.")
        End If

    End Sub


    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click

        If currentIndex > 0 Then
            currentIndex -= 1
            DisplayRecord(currentIndex)
        Else
            MessageBox.Show("You are at the first record.")
        End If

    End Sub

End Class
