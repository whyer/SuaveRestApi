open SuaveRestApi.Rest
open SuaveRestApi.Db
open SuaveRestApi.MusicStoreDb
open Suave
open Suave.Web

[<EntryPoint>]
let main argv = 
    let personWebPart = rest "person" {
      GetAll = Db.getPeople
      Create = Db.createPerson
      Update = Db.updatePerson
      Delete = Db.deletePersonById
      GetById = Db.getPersonById
      UpdateByID = Db.updatePersonById
      Exists = Db.doesPersonExists
    }

    let albumWebPart = rest "album" {
      GetAll = MusicStoreDb.getAlbums
      Create = MusicStoreDb.createAlbum
      Update = MusicStoreDb.updateAlbum
      UpdateByID = MusicStoreDb.updateAlbumById
      Delete = MusicStoreDb.deleteAlbum
      GetById = MusicStoreDb.getAlbumById
      Exists = MusicStoreDb.isAlbumExists
    }

    startWebServer defaultConfig (choose [personWebPart; albumWebPart])
    0 
