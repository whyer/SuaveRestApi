open SuaveRestApi.Rest
open SuaveRestApi.Db
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

    startWebServer defaultConfig personWebPart
    0 
