import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
// import Contacts from './components/Contacts';
// import Help from './components/Help';
// import Statistics from './components/Statistics';
import RentalTable from './components/RentalTable';

const App = () => {
    return (
        <Router>
            <Navbar />
            <Routes>
                  <Route path="/Rental" element={
                    <div style={{ margin: '50px 100px' }}>
                        <RentalTable/>
                    </div>} />
                {/* <Route path="/Contacts" element={<Contacts />} />
                <Route path="/Help" element={<Help />} />
                <Route path="/Statistics" element={<Statistics />} /> */}
            </Routes>
        </Router>
    );
};

export default App;
