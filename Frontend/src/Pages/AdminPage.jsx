import React, {useState} from 'react';
import CityEdit from "../Components/CityAdmin/CityEdit.jsx";
import CityDisplay from "../Components/CityAdmin/CityDisplay.jsx";
import SolarDataEdit from "../Components/SolarDataAdmin/SolarDataEdit.jsx";
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
            return isEditMode ? <CityEdit setIsEditMode={setIsEditMode}/> : <CityDisplay setIsEditMode={setIsEditMode}/>;
        }
        if (view === "solarData") {
            return isEditMode ? <SolarDataEdit setIsEditMode={setIsEditMode}/> : <SolarDataDisplay setIsEditMode={setIsEditMode}/>;
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