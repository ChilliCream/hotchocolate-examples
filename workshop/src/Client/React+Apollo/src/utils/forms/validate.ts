import { setIn } from "final-form";

const validate = async (values, validationSchema) => {
  try {
    await validationSchema.validate(values, { abortEarly: false });
  } catch (err) {
    const errors = err.inner.reduce((formError, innerError) => {
      return setIn(formError, innerError.path, innerError.message);
    }, {});

    return errors;
  }
};

export { validate };
