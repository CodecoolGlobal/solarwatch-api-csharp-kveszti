import React, {useState} from 'react';

import CityDisplay from "../Components/CityAdmin/CityDisplay.jsx";

import SolarDataDisplay from "../Components/SolarDataAdmin/SolarDataDisplay.jsx";

function AdminPage(props) {
    const [view, setView] = useState(null) //can be set to "city" or "solarData"
    const [isEditMode, setIsEditMode] = useState(false);
    
    function renderLogic(){
        if (!view) {
            return <div className='containerDiv'>
                <div className='chooseViewText'>Please choose above the view you'd like to see.</div>
            </div>;
        }
        if (view === "city") {
            return <CityDisplay setIsEditMode={setIsEditMode} isEditMode={isEditMode}/>;
        }
        if (view === "solarData") {
            return  <SolarDataDisplay setIsEditMode={setIsEditMode} isEditMode={isEditMode}/>;
        }
        return <div>Something went wrong...</div>;
    }
    
    return (
        <>
            <div className='buttonContainer'>
                {view !== "city" ? <button className='viewButton' onClick={() => setView(() => "city")}>City editor</button> : ''}
                {view !== "solarData" ? <button className='viewButton' onClick={() => setView(() => "solarData")}>Solar data editor</button> : ''}
            </div>
            {renderLogic()}
        </>
    )

}

export default AdminPage;