import React, {useEffect, useState} from 'react';
import SolarDataDisplayContainer from "../SolarDataAdmin/SolarDataDisplayContainer.jsx";

function SolarDataDisplay({setIsEditMode}) {
    const [solarData, setSolarData] = useState(null);

    useEffect(() => {
        async function fetchSolarData(){
            const url = "api/SolarData/GetAllSolarData";
            const token = localStorage.getItem('token');
            const response = await fetch(url,{
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            });
            const data = await response.json();
            setSolarData(data);
            console.log(data);
        }
        fetchSolarData();
    }, []);

    async function handleSolarDataDelete(id){
        const url = `api/SolarData/DeleteSolarData?id=${id}`;
        const token = localStorage.getItem('token');

        const response = await fetch(url,{
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });

        if(response.ok){
            const newFilteredArr = solarData.filter(data => data.id !== id);
            setSolarData(() => newFilteredArr);
        }else {
            console.log("Something went wrong while deleting the city...")
        }
    }
    
    return (
        <div className='containerDiv'>
            {solarData ? (solarData.map(data => <SolarDataDisplayContainer
                    key={data.id} solarData={data} setIsEditMode={setIsEditMode} handleSolarDataDelete={handleSolarDataDelete}/>)) :
                <div>Loading...</div>}
        </div>
    );
}

export default SolarDataDisplay;