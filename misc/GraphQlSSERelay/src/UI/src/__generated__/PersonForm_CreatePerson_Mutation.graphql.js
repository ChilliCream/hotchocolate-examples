/**
 * @generated SignedSource<<d6aacc38d47661ce2e30d30b799f8d08>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* eslint-disable */

'use strict';

var node = (function(){
var v0 = [
  {
    "defaultValue": null,
    "kind": "LocalArgument",
    "name": "input"
  }
],
v1 = [
  {
    "alias": null,
    "args": [
      {
        "kind": "Variable",
        "name": "input",
        "variableName": "input"
      }
    ],
    "concreteType": "AddPersonPayload",
    "kind": "LinkedField",
    "name": "addPerson",
    "plural": false,
    "selections": [
      {
        "alias": null,
        "args": null,
        "concreteType": "Person",
        "kind": "LinkedField",
        "name": "person",
        "plural": false,
        "selections": [
          {
            "alias": null,
            "args": null,
            "kind": "ScalarField",
            "name": "name",
            "storageKey": null
          }
        ],
        "storageKey": null
      }
    ],
    "storageKey": null
  }
];
return {
  "fragment": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Fragment",
    "metadata": null,
    "name": "PersonForm_CreatePerson_Mutation",
    "selections": (v1/*: any*/),
    "type": "Mutation",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Operation",
    "name": "PersonForm_CreatePerson_Mutation",
    "selections": (v1/*: any*/)
  },
  "params": {
    "cacheID": "a2d522f541f2573fc5e4eb481d1624f4",
    "id": null,
    "metadata": {},
    "name": "PersonForm_CreatePerson_Mutation",
    "operationKind": "mutation",
    "text": "mutation PersonForm_CreatePerson_Mutation(\n  $input: AddPersonInput!\n) {\n  addPerson(input: $input) {\n    person {\n      name\n    }\n  }\n}\n"
  }
};
})();

node.hash = "3495de6a6c43888c08a99a5da60c9297";

module.exports = node;
