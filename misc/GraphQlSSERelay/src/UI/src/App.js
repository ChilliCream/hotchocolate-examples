import { PersonListener } from "./PersonListener";
import { PersonForm } from "./PersonForm";
import "./App.css";
import RelayEnviornment from "./RelayEnviornment";
import { RelayEnvironmentProvider } from "react-relay";

function App() {
  return (
    <RelayEnvironmentProvider environment={RelayEnviornment}>
      <div className="App">
        <PersonListener />
        <PersonForm />
      </div>
    </RelayEnvironmentProvider>
  );
}

export default App;
