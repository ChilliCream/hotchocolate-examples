import cloneDeep from "lodash-es/cloneDeep";

const addToCache = (client, query, objectName, newData, queryDataKey, variables = {}) => {
  const data = cloneDeep(
    client.readQuery({
      query,
      variables,
    })
  );
  data[objectName].push(newData.data[queryDataKey]);
  client.writeQuery({
    query,
    variables,
    data,
  });
};

export { addToCache };
