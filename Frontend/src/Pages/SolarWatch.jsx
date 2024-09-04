import DatePicker from "react-datepicker";
import {useEffect, useState} from "react";
import SearchSolarData from "../Components/SearchSolarData.jsx";
import DisplaySolarData from "../Components/DisplaySolarData.jsx";

export default function SolarWatch({StyledForm, FormLabel, TextInput, SubmitButton, FormContainerDiv}){
    const [isDisplayed, setIsDisplayed] = useState(false);
    const [dataToDisplay, setDataToDisplay] = useState("");
    const [cityToDisplay, setCityToDisplay] = useState("");
    const [typeOfSunData, setTypeOfSunData] = useState("");
    const [isSomethingWrong, setIsSomethingWrong] = useState(false)
    
    async function FetchSolarData(e, city, date, sunData){
      e.preventDefault();
      
      let url = "";
      setIsSomethingWrong(()=> false);
            url = `api/SunriseSunset/Get${sunData}?city=${encodeURIComponent(city)}&timeZone=${encodeURIComponent("Europe/Budapest")}`
        
        if (date) {
            url += `&date=${encodeURIComponent(date)}`;
        }

        const token = localStorage.getItem('token');
    
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });
        
        if (response.ok) {
            const data = await response.json();
            setCityToDisplay(() => city);
            setDataToDisplay(()=> data);
            setTypeOfSunData(() => sunData)
            setIsDisplayed(() => true);
        }
            
        
        else {
            setIsSomethingWrong(true);
        }
    }
    

    useEffect(() => {
        if(isDisplayed){
            setIsSomethingWrong(() => false);
        }
    }, [isDisplayed]);
    
    return(
        <> {isDisplayed? <DisplaySolarData dataToDisplay={dataToDisplay} cityToDisplay={cityToDisplay} typeOfSunData={typeOfSunData} setIsDisplayed={setIsDisplayed}/>: <SearchSolarData StyledForm={StyledForm} FormLabel={FormLabel} TextInput={TextInput} SubmitButton={SubmitButton} FormContainerDiv={FormContainerDiv} fetchSolarData={FetchSolarData} isSomethingWrong={isSomethingWrong}/>}
        </>
    )
}