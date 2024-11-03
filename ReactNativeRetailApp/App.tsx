import { StrictMode } from 'react';
import { Provider } from "react-redux";
import Routes from "./routes/Routes";
import store from "./states/Store";

export default function App() {
  return (
    <StrictMode>
      <Provider store={store}>
        <Routes />
      </Provider>
    </StrictMode>
  );
}
