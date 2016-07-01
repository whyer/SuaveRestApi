namespace SuaveRestApi.Db
open System.Collections.Generic

type Person = {
  Id: int
  Name: string
  Age: int
  Email: string
}

module Db =
  let private peopleStorage = new Dictionary<int, Person>()
  let getPeople () =
    peopleStorage.Values :> seq<Person>

  let create person =
    let nextId = peopleStorage.Values.Count + 1
    let newPerson = {person with Id = nextId}
    peopleStorage.Add(nextId, newPerson)
    newPerson
  

