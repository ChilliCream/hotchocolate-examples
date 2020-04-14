using System;
using HotChocolate;
using HotChocolate.Stitching;
using HotChocolate.Types;
using HotChocolate.Language;

namespace Demo.Stitching
{
    public class AddCreatedByIdQueryRewriter
        : QueryDelegationRewriterBase
{
    public override SelectionSetNode OnRewriteSelectionSet(
        NameString targetSchemaName,
        IOutputType outputType,
        IOutputField outputField,
        SelectionSetNode selectionSet)
    {
        if(outputType.NamedType() is ObjectType objectType
          && objectType.Name.Equals("Customer"))
        {
            return selectionSet.AddSelection(
                new FieldNode
                (
                    null,
                    new NameNode("id"),
                    new NameNode("_internalId"),
                    Array.Empty<DirectiveNode>(),
                    Array.Empty<ArgumentNode>(),
                    null
                ));
        }

        return selectionSet;
    }
}
}
