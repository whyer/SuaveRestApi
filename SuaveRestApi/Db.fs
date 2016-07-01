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

  let createPerson person =
    let nextId = peopleStorage.Values.Count + 1
    let newPerson = {person with Id = nextId}
    peopleStorage.Add(nextId, newPerson)
    newPerson
  
  let updatePersonById personId personToUpdate =
    if peopleStorage.ContainsKey personId then
      let updatedPerson = {personToUpdate with Id = personId}
      peopleStorage.[personId] <- updatedPerson
      Some updatedPerson
    else
      None

  let updatePerson personToUpdate =
    updatePersonById personToUpdate.Id personToUpdate

  let deletePersonById personId =
    peopleStorage.Remove(personId) |> ignore

  let getPersonById personId =
    if peopleStorage.ContainsKey(personId) then
      Some peopleStorage.[personId]
    else
      None

  let doesPersonExists personId =
    peopleStorage.ContainsKey(personId)