/**
 * @generated SignedSource<<746c76c0a9856dec3b83b49bbea77862>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* eslint-disable */

'use strict';

var node = (function(){
var v0 = [
  {
    "alias": null,
    "args": null,
    "concreteType": "Person",
    "kind": "LinkedField",
    "name": "personAdded",
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
];
return {
  "fragment": {
    "argumentDefinitions": [],
    "kind": "Fragment",
    "metadata": null,
    "name": "PersonListener_PersonAdded_Subscription",
    "selections": (v0/*: any*/),
    "type": "Subscription",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": [],
    "kind": "Operation",
    "name": "PersonListener_PersonAdded_Subscription",
    "selections": (v0/*: any*/)
  },
  "params": {
    "cacheID": "0b6fb027104932c5feee895e5e722795",
    "id": null,
    "metadata": {},
    "name": "PersonListener_PersonAdded_Subscription",
    "operationKind": "subscription",
    "text": "subscription PersonListener_PersonAdded_Subscription {\n  personAdded {\n    name\n  }\n}\n"
  }
};
})();

node.hash = "ed3f11091eb48308acbc1871a9730773";

module.exports = node;
