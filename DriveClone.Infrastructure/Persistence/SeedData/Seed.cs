using DriveClone.Domain.Models;
using DriveClone.Domain.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace DriveClone.Infrastructure.Persistence.SeedData;

public class Seed(DataContext context)
{
    public void SeedDataContext(UserManager<User> userManager)
    {
        // //dotnet run -- seeddata  used inside driveclone.webapi folder
        // if (context.Users.Any())
        // {
        //     var currentDate = DateTimeOffset.UtcNow.DateTime;
        //
        //     // 1. Create Users
        //     var user1 = new User { UserName = "alice.smith@example.com", Email = "alice.smith@example.com", FirstName = "Alice", LastName = "Smith" };
        //     var user2 = new User { UserName = "bob.jones@example.com", Email = "bob.jones@example.com", FirstName = "Bob", LastName = "Jones" };
        //     var user3 = new User { UserName = "carol.lee@example.com", Email = "carol.lee@example.com", FirstName = "Carol", LastName = "Lee" };
        //
        //     // userManager.CreateAsync(user1, "Password123!").Wait();
        //     // userManager.CreateAsync(user2, "Password123!").Wait();
        //     // userManager.CreateAsync(user3, "Password123!").Wait();
        //     
        //     Console.WriteLine("users created no prob");
        //     context.SaveChanges();
        //
        //     var dbUser1 = context.Users.First(u => u.UserName == user1.UserName);
        //     var dbUser2 = context.Users.First(u => u.UserName == user2.UserName);
        //     var dbUser3 = context.Users.First(u => u.UserName == user3.UserName);
        //
        //     // 2. Create Folders
        //     
        //     var folder1 = new Folder { PubId = Guid.NewGuid().ToString(), Name = "Documents", ParentId = null, CreatedBy = dbUser1.Id, CreatedDate = currentDate };
        //     context.Folders.Add(folder1);
        //     context.SaveChanges();
        //
        //     var folder2 = new Folder { PubId = Guid.NewGuid().ToString(), Name = "Images", ParentId = folder1.Id, CreatedBy = dbUser1.Id, CreatedDate = currentDate };
        //     var folder3 = new Folder { PubId = Guid.NewGuid().ToString(), Name = "Work",   ParentId = folder1.Id, CreatedBy = dbUser2.Id, CreatedDate = currentDate };
        //     context.Folders.AddRange(folder2, folder3);
        //     context.SaveChanges();
        //
        //     var folder4 = new Folder { PubId = Guid.NewGuid().ToString(), Name = "Invoices", ParentId = folder2.Id, CreatedBy = dbUser2.Id, CreatedDate = currentDate };
        //     context.Folders.Add(folder4);
        //     context.SaveChanges();
        //
        //     // 3. Create UserFolders
        //     var userfolder1 = new UserFolder { PubId = Guid.NewGuid().ToString(), FolderId = folder1.Id, UserId = dbUser1.Id, IsOwner = true,  AccessPermission = AccessPermission.ReadWrite, CreatedBy = dbUser1.Id, CreatedDate = currentDate };
        //     var userfolder2 = new UserFolder { PubId = Guid.NewGuid().ToString(), FolderId = folder2.Id, UserId = dbUser1.Id, IsOwner = true,  AccessPermission = AccessPermission.ReadWrite, CreatedBy = dbUser1.Id, CreatedDate = currentDate };
        //     var userfolder3 = new UserFolder { PubId = Guid.NewGuid().ToString(), FolderId = folder3.Id, UserId = dbUser2.Id, IsOwner = true,  AccessPermission = AccessPermission.ReadWrite, CreatedBy = dbUser2.Id, CreatedDate = currentDate };
        //     var userfolder4 = new UserFolder { PubId = Guid.NewGuid().ToString(), FolderId = folder4.Id, UserId = dbUser2.Id, IsOwner = true,  AccessPermission = AccessPermission.ReadWrite, CreatedBy = dbUser2.Id, CreatedDate = currentDate };
        //     var userfolder5 = new UserFolder { PubId = Guid.NewGuid().ToString(), FolderId = folder1.Id, UserId = dbUser2.Id, IsOwner = false, AccessPermission = AccessPermission.Read,      CreatedBy = dbUser1.Id, CreatedDate = currentDate };
        //     var userfolder6 = new UserFolder { PubId = Guid.NewGuid().ToString(), FolderId = folder3.Id, UserId = dbUser3.Id, IsOwner = false, AccessPermission = AccessPermission.Read,      CreatedBy = dbUser2.Id, CreatedDate = currentDate };
        //
        //     context.UserFolders.AddRange(userfolder1, userfolder2, userfolder3, userfolder4, userfolder5, userfolder6);
        //     context.SaveChanges();
        //     Console.WriteLine("userfolders created no prob");
        //
        //     // 4. Create FileMetaData
        //     var file1 = new FileMetaData
        //     {
        //         PubId = Guid.NewGuid().ToString(),
        //         FileName = "resume.txt",
        //         FilePath = "documents/resume.txt",
        //         FileType = FileType.TXT,
        //         CreatedBy = dbUser1.Id,
        //         CreatedDate = currentDate,
        //         OwnerId = dbUser1.Id,
        //         ParentFolderId = folder1.Id
        //     };
        //
        //     var file2 = new FileMetaData
        //     {
        //         PubId = Guid.NewGuid().ToString(),
        //         FileName = "profile-picture.png",
        //         FilePath = "images/profile-picture.png",
        //         FileType = FileType.Image,
        //         CreatedBy = dbUser1.Id,
        //         CreatedDate = currentDate,
        //         OwnerId = dbUser1.Id,
        //         ParentFolderId = folder2.Id
        //     };
        //
        //     var file3 = new FileMetaData
        //     {
        //         PubId = Guid.NewGuid().ToString(),
        //         FileName = "meeting-notes.txt",
        //         FilePath = "work/meeting-notes.txt",
        //         FileType = FileType.TXT,
        //         CreatedBy = dbUser2.Id,
        //         CreatedDate = currentDate,
        //         OwnerId = dbUser2.Id,
        //         ParentFolderId = folder3.Id
        //     };
        //
        //     var file4 = new FileMetaData
        //     {
        //         PubId = Guid.NewGuid().ToString(),
        //         FileName = "invoice-2024-001.pdf",
        //         FilePath = "invoices/invoice-2024-001.pdf",
        //         FileType = FileType.PDF,
        //         CreatedBy = dbUser2.Id,
        //         CreatedDate = currentDate,
        //         OwnerId = dbUser2.Id,
        //         ParentFolderId = folder4.Id
        //     };
        //
        //     var file5 = new FileMetaData
        //     {
        //         PubId = Guid.NewGuid().ToString(),
        //         FileName = "shared-report.pdf",
        //         FilePath = "documents/shared-report.pdf",
        //         FileType = FileType.PDF,
        //         CreatedBy = dbUser1.Id,
        //         CreatedDate = currentDate,
        //         OwnerId = dbUser1.Id,
        //         ParentFolderId = folder1.Id
        //     };
        //
        //     context.FilesMetaData.AddRange(file1, file2, file3, file4, file5);
        //     context.SaveChanges();
        //     Console.WriteLine("metadata created no prob");
        // }
    }
}
