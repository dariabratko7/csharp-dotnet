using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncNotesApp
{ 
    public class Note
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
         
        public Note() { }

        public Note(string title, string text)
        {
            Title = title;
            Text = text;
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{CreatedAt:yyyy-MM-dd HH:mm:ss}] {Title}: {Text}";
        }
    }
     
    public class NoteManager
    {
        private readonly List<Note> _notes = new();

        public IReadOnlyList<Note> Notes => _notes.AsReadOnly();
         

        public Note AddNote(string title, string text)
        {
            var note = new Note(title, text);
            _notes.Add(note);
            return note;
        }

        public bool DeleteNote(string title)
        {
            var note = _notes.FirstOrDefault(n => n.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (note != null)
            {
                _notes.Remove(note);
                return true;
            }
            return false;
        }

        public bool EditNote(string title, string newText)
        {
            var note = _notes.FirstOrDefault(n => n.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (note != null)
            {
                note.Text = newText;
                return true;
            }
            return false;
        }
         
        public async Task SaveNotesSequentialAsync(string folderPath = "notes_seq")
        {
            Directory.CreateDirectory(folderPath);
            Console.WriteLine("\n[ПОСЛІДОВНО] Початок збереження нотаток...");

            var sw = Stopwatch.StartNew();
            var options = new JsonSerializerOptions { WriteIndented = true };

            foreach (var note in _notes)
            {
                string filePath = Path.Combine(folderPath, $"{SanitizeFileName(note.Title)}.json");
                string jsonString = JsonSerializer.Serialize(note, options);
                 
                await File.WriteAllTextAsync(filePath, jsonString);
                Console.WriteLine($"  -> Збережено: {Path.GetFileName(filePath)}");
            }

            sw.Stop();
            Console.WriteLine($"[ПОСЛІДОВНО] Завершено за {sw.ElapsedMilliseconds} мс.");
        }
         
        public async Task SaveNotesParallelAsync(string folderPath = "notes_par")
        {
            Directory.CreateDirectory(folderPath);
            Console.WriteLine("\n[ПАРАЛЕЛЬНО] Початок збереження нотаток...");

            var sw = Stopwatch.StartNew();
            var options = new JsonSerializerOptions { WriteIndented = true };
            var saveTasks = new List<Task>();

            foreach (var note in _notes)
            {
                string filePath = Path.Combine(folderPath, $"{SanitizeFileName(note.Title)}.json");
                string jsonString = JsonSerializer.Serialize(note, options);
                 
                Task task = File.WriteAllTextAsync(filePath, jsonString);
                saveTasks.Add(task);
            }
             
            await Task.WhenAll(saveTasks);

            sw.Stop();
            Console.WriteLine($"[ПАРАЛЕЛЬНО] Завершено збереження {saveTasks.Count} файлів за {sw.ElapsedMilliseconds} мс.");
        }
         
        public async Task LoadNotesParallelAsync(string folderPath = "notes_par")
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Директорію не знайдено.");
                return;
            }

            string[] filePaths = Directory.GetFiles(folderPath, "*.json");
            if (filePaths.Length == 0)
            {
                Console.WriteLine("Файлів для завантаження немає.");
                return;
            }

            Console.WriteLine("\n[ПАРАЛЕЛЬНО] Початок завантаження нотаток...");
             
            IEnumerable<Task<Note?>> loadTasks = filePaths.Select(async filePath =>
            {
                string jsonString = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<Note>(jsonString);
            });
             
            Note?[] loadedNotes = await Task.WhenAll(loadTasks);

            _notes.Clear();
            foreach (var note in loadedNotes)
            {
                if (note != null)
                {
                    _notes.Add(note);
                }
            }

            Console.WriteLine($"[ПАРАЛЕЛЬНО] Завантажено {_notes.Count} нотаток у менеджер.");
        }

        private static string SanitizeFileName(string name)
        {
            return string.Join("_", name.Split(Path.GetInvalidFileNameChars()));
        }
    }
     
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var manager = new NoteManager();
             
            manager.AddNote("Покупки", "Купити молоко, хліб, каву");
            manager.AddNote("Навчання", "Зробити лабу з асинхронності на C#");
            manager.AddNote("Проект", "Реалізувати клас NoteManager");
            manager.AddNote("Ідеї", "Вивчити роботу Task.WhenAll");
             
            manager.EditNote("Покупки", "Купити молоко, хліб, каву та фрукти");
             
            await manager.SaveNotesSequentialAsync("notes_seq");
             
            await manager.SaveNotesParallelAsync("notes_par");
             
            var newManager = new NoteManager();
            await newManager.LoadNotesParallelAsync("notes_par");

            Console.WriteLine("\nСписок завантажених нотаток:");
            foreach (var note in newManager.Notes)
            {
                Console.WriteLine($"- {note}");
            }
        }
    }
}
