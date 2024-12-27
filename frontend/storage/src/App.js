import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Navbar from './components/Navbar';
import Contacts from './components/Contacts';
import Help from './components/Help';
import Statistics from './components/Statistics';
import RentalTable from './components/RentalTable';
import ManageTables from './components/ManageTables';

const App = () => {
    return (
        <Router>
            <Navbar />
            <Routes>
                <Route path="/" element={<Navigate to = "/Rental"/>}>
                    <Route 
                        path="*"
                        element={<Navigate to="/"/>}
                    />
                </Route>
                <Route path="/Rental" element={
                    <div style={{ margin: '50px 75px' }}>
                        <RentalTable/>
                    </div>} />
                <Route path="/Contacts" element={<Contacts />} />
                <Route path="/Help" element={<Help />} />
                <Route path="/Statistics" element={<Statistics />} />
                <Route path="/ManageTables" element={
                    <div style={{margin: '25px 200px'}}>
                        <ManageTables/>
                    </div>}/>
            </Routes>
        </Router>
    );
};

export default App;
