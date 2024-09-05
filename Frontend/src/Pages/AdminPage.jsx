import React, {useState} from 'react';
import CityEdit from "../Pages/CityEdit.jsx";
import CityDisplay from "../Pages/CityDisplay.jsx";
import SolarDataEdit from "../Pages/SolarDataEdit.jsx";
import SolarDataDisplay from "../Pages/SolarDataDisplay.jsx";

function AdminPage(props) {
    const [view, setView] = useState(null) //can be set to "city" or "solarData"
    const [isEditMode, setIsEditMode] = useState(false);
    
    function renderLogic(){
        if (!view) {
            return <div>Please choose which view you'd like to see.</div>;
        }
        if (view === "city") {
            return isEditMode ? <CityEdit /> : <CityDisplay />;
        }
        if (view === "solarData") {
            return isEditMode ? <SolarDataEdit/> : <SolarDataDisplay/>;
        }
        return <div>Something went wrong...</div>;
    }
    
    return (
        <>
            <button onClick={() => setView(() => "city")}>City editor</button>
            <button onClick={() => setView(() => "solarData")}>Solar data editor</button>
            {renderLogic()}
        </>
    )

}

export default AdminPage;