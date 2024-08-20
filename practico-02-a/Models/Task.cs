using System.Text.Json.Serialization;

namespace practico_02_a.Models;

public class Task
{
   public long Id { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }
   public int Duration { get; set; }
   public string? Responsible { get; set; }

   public Task()
   {
   }

   public Task(long id, string? name, string? description, int duration, string? responsible)
   {
      Id = id;
      Name = name;
      Description = description;
      Duration = duration;
      Responsible = responsible;
   }

   public override string ToString()
   {
      return $"id: {Id} \n" +
             $"name: {Name} \n" +
             $"description: {Description} \n" +
             $"duration: {Duration} \n" +
             $"responsible: {Responsible} \n";
   }
}