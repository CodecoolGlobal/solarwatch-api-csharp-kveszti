import {useState} from "react";
import styled from "styled-components";

export default function SearchSolarData({StyledForm, FormLabel, TextInput, SubmitButton, FormContainerDiv, fetchSolarData, isSomethingWrong}){
   
    const [date, setDate] = useState();
    const [city, setCity] = useState();
    const [sunData, setSunData] = useState("Sunrise");
    const ErrorMsg = styled.p`
    color:red;`
    
    return(
        <>
            <h2>Welcome to the main page!</h2>
            <p>Hello there! The task is simple: Please give the name of your chosen city, a date and whether you'd like to see the sunrise or
                the sunset data. Press "search" and enjoy the data!</p>
            <FormContainerDiv>
                <StyledForm onSubmit={(e) => fetchSolarData(e, city, date, sunData)}>
                    <FormLabel htmlFor="cityName">City:</FormLabel>
                    <TextInput name="cityName" onChange={(e) => setCity(e.target.value)}></TextInput>
                    <FormLabel htmlFor="date">Date:</FormLabel>
                    <TextInput type="date" name="date" onChange={(e) => setDate(e.target.value)}></TextInput>
                    <FormLabel htmlFor="sunData">Sunrise or sunset:</FormLabel>
                    <select className="formSelect" onChange={(e) => setSunData(e.target.value)}>
                        <option>Sunrise</option>
                        <option>Sunset</option>
                    </select>
                    {isSomethingWrong? <ErrorMsg>Please review you search details as we cannot provide data with the given parameters </ErrorMsg> : ''}
                    <SubmitButton>Search</SubmitButton>
                </StyledForm>
            </FormContainerDiv>
        </>
    )
}